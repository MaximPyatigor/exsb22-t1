using BudgetManager.CQRS.Commands.WalletCommands;
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

        public DeleteUserWalletHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserWalletCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);

            if (request.walletId == user?.DefaultWallet)
            {
                throw new AppException("Default wallet cannot be deleted");
            }

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var update = Builders<User>.Update.PullFilter(u => u.Wallets, w => w.Id == request.walletId);
            var result = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);

            return result is not null;
        }
    }
}
