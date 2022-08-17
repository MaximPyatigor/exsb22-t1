using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetAllWalletsListQuery(Guid userId) : IRequest<IEnumerable<WalletResponse>>;
}
