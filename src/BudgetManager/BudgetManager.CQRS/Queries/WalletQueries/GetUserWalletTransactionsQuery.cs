using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.WalletQueries
{
    public record GetUserWalletTransactionsQuery(Guid userId, Guid walletId) : IRequest<WalletTransactionsDTO>;
}
