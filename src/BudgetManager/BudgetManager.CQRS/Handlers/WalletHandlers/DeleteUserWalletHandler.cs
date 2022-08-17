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

            var walletTransactions = await _transactionRepository.GetListByWalletIdAsync(wallet.Id, cancellationToken);
            User? walletResult;

            if (walletTransactions is null || walletTransactions.Count() == 0)
            {
                var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
                var userUpdate = Builders<User>.Update.PullFilter(u => u.Wallets, w => w.Id == request.walletId);
                walletResult = await _userRepository.UpdateOneAsync(userFilter, userUpdate, cancellationToken);
            }
            else
            {
                walletResult = await _userRepository.UpdateOneAsync(
                u => u.Id == request.userId && u.Wallets.Any(w => w.Id == request.walletId),
                Builders<User>.Update.Set(w => w.Wallets[-1].IsActive, false), cancellationToken);
            }

            return walletResult is not null;
        }
    }
}
