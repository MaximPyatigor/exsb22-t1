using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class DeleteUserWalletHandler : IRequestHandler<DeleteUserWalletCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DeleteUserWalletHandler(IBaseRepository<User> userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteUserWalletCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (request.walletId == user.DefaultWallet)
            {
                throw new AppException("Default wallet cannot be deleted");
            }

            var wallet = user.Wallets?.FirstOrDefault(w => w.Id == request.walletId);

            if (wallet is null)
            {
                throw new KeyNotFoundException("Wallet not found");
            }

            var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var userUpdate = Builders<User>.Update.PullFilter(u => u.Wallets, w => w.Id == request.walletId);
            var walletResult = await _userRepository.UpdateOneAsync(userFilter, userUpdate, cancellationToken);

            var transactionFilter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.Eq(t => t.UserId, request.userId),
                Builders<Transaction>.Filter.Eq(t => t.WalletId, request.walletId));
            var transactionResult = await _transactionRepository.DeleteManyAsync(transactionFilter, cancellationToken);

            return walletResult is not null && transactionResult;
        }
    }
}
