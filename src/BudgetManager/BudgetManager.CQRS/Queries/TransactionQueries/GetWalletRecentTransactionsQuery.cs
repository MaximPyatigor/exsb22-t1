using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetWalletRecentTransactionsQuery(Guid userId, WalletRecentTransactionsPageDTO recentTransactionsPageDTO) : IRequest<RecentTransactionsPageResponse>;
}
