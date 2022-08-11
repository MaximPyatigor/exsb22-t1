using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletInfoHandler : IRequestHandler<GetWalletInfoQuery, WalletViewResponse>
    {
        private readonly IBaseRepository<User> _userRepository;

        public GetWalletInfoHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<WalletViewResponse> Handle(GetWalletInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var userWallet = user.Wallets?.FirstOrDefault(w => w.Id == request.walletId);

            if (userWallet is null)
            {
                throw new KeyNotFoundException("Wallet not found");
            }

            var response = new WalletViewResponse
            {
                WalletId = userWallet.Id,
                WalletName = userWallet.Name,
                Balance = userWallet.Balance,
                CurrencyCode = userWallet.Currency.CurrencyCode,
                IsDefault = userWallet.Id == user.DefaultWallet,
            };

            return response;
        }
    }
}
