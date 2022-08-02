using BudgetManager.CQRS.Responses.CurrencyResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.CurrencyQueries
{
    public record GetCurrencyListWithDefaultOnTopQuery(Guid userId) : IRequest<IEnumerable<CurrencyResponse>>;
}
