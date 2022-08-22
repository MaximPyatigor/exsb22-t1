using BudgetManager.CQRS.Responses.PageResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetExpenseTransactionListQuery(Guid userId, ExpensesPageDTO expensesPageDto) : IRequest<PageResponse<ExpenseTransactionResponse>>;
}
