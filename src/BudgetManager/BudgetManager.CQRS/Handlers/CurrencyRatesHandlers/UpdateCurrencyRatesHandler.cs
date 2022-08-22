using BudgetManager.CQRS.Commands.CurrencyRatesCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyRatesHandlers
{
    public class UpdateCurrencyRatesHandler : IRequestHandler<UpdateCurrencyRatesCommand>
    {
        private readonly IBaseRepository<CurrencyRates> _dataAccess;

        public UpdateCurrencyRatesHandler(IBaseRepository<CurrencyRates> dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Unit> Handle(UpdateCurrencyRatesCommand request, CancellationToken cancellationToken)
        {
            await _dataAccess.UpsertAsync(request.CurrencyRates, CancellationToken.None);
            return Unit.Value;
        }
    }
}
