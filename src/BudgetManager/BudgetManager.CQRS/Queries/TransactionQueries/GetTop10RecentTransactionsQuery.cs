using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTop10RecentTransactionsQuery(Guid userId) : IRequest<List<TransactionResponse>>;
}
