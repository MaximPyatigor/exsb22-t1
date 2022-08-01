using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetWalletListQuery(Guid userId) : IRequest<IEnumerable<WalletResponse>>;
}
