using BudgetManager.CQRS.Commands.CurrencyCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class AddManyCurrenciesHandler : IRequestHandler<AddManyCurrenciesCommand, IEnumerable<Guid>>
    {
        private readonly ICurrencyRepository _dataAccess;

        public AddManyCurrenciesHandler(ICurrencyRepository dataAccess)
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