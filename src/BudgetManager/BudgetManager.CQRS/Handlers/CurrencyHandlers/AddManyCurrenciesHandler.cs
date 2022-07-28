using BudgetManager.CQRS.Commands.CurrencyCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class AddManyCurrenciesHandler : IRequestHandler<AddManyCurrenciesCommand, IEnumerable<Guid>>
    {
        private readonly IBaseRepository<Currency> _dataAccess;

        public AddManyCurrenciesHandler(IBaseRepository<Currency> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyCurrenciesCommand request, CancellationToken cancellationToken)
        {
            var currencies = request.currencies;
            await _dataAccess.InsertManyAsync(currencies, cancellationToken);

            return currencies.Select(c => c.Id);
        }
    }
}