using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetExpenseTransactionListQuery(Guid userId) : IRequest<IEnumerable<ExpenseTransactionResponse>>;
}
