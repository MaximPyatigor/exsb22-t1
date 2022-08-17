using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetActiveWalletsListQuery(Guid userId) : IRequest<IEnumerable<WalletResponse>>;
}
