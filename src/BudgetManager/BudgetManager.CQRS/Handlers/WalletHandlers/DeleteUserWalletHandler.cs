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

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (request.walletId == user.DefaultWallet)
            {
                throw new AppException("Default wallet cannot be deleted");
            }

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var newWallets = new List<Wallet>();
            if (user.Wallets is not null)
            {
                foreach (var wallet in user.Wallets)
                {
                    if (wallet.Id == request.walletId)
                    {
                        wallet.IsActive = false;
                    }

                    newWallets.Add(wallet);
                }
            }

            var update = Builders<User>.Update.Set(w => w.Wallets, newWallets);
            var result = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);

            return result is not null;
        }
    }
}
