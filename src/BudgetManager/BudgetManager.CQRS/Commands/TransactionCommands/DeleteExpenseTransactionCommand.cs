using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record DeleteExpenseTransactionCommand(Guid userId, Guid expenseId) : IRequest<bool>;
}
