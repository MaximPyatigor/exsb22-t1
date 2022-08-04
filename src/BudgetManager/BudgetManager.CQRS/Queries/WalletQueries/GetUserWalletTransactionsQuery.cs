using BudgetManager.CQRS.Responses.WalletResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetUserWalletTransactionsQuery(Guid userId, Guid walletId) : IRequest<WalletTransactionsResponse>;
}
