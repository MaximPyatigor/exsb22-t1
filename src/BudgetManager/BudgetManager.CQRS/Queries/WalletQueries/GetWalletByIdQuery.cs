using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetWalletByIdQuery(Guid userId, Guid walletId) : IRequest<WalletResponse>;
}