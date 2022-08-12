using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetRecentTransactionsQuery(Guid userId) : IRequest<List<RecentTransactionsHomepageResponse>>;
}
