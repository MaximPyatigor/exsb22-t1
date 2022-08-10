using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Queries.CurrencyQueries
{
    public record GetCurrencyByIdQuery(Guid Id) : IRequest<Currency>;
}
