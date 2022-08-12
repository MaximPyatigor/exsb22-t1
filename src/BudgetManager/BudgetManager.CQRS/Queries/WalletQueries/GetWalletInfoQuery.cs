using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetWalletInfoQuery(Guid userId, Guid walletId) : IRequest<WalletViewResponse>;
}
