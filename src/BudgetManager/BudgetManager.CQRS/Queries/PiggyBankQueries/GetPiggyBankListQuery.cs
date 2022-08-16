using BudgetManager.CQRS.Responses.PiggyBankResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.PiggyBankQueries
{
    public record GetPiggyBankListQuery(Guid userId) : IRequest<IEnumerable<PiggyBankResponse>>;
}
