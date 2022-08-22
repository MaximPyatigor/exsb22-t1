using BudgetManager.CQRS.Queries.CurrencyRatesQueries;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class CalculateUserTotalBalanceHandler : IRequestHandler<CalculateUserTotalBalanceQuery, decimal>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public CalculateUserTotalBalanceHandler(IBaseRepository<User> userRepository,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<decimal> Handle(CalculateUserTotalBalanceQuery request, CancellationToken cancellationToken)
        {
            var convertTo = request.currencyCode;
            var userId = request.userId;
            decimal totalBalance = 0;
            var userWallets = await _mediator.Send(new GetActiveWalletsListQuery(userId), cancellationToken);

            foreach (var wallet in userWallets)
            {
                var amount = await _mediator.Send(
                    new ConvertCurrencyRatesQuery(wallet.Currency.CurrencyCode, convertTo, wallet.Balance),
                    cancellationToken);

                totalBalance += amount;
            }

            return totalBalance;
        }
    }
}
