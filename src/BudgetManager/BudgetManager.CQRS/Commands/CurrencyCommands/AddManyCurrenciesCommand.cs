using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.CurrencyCommands
{
    public record AddManyCurrenciesCommand(IEnumerable<Currency> currencies) : IRequest<IEnumerable<Guid>>;
}