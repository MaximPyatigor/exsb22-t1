using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddExpenseTransactionCommand(Guid userId, AddExpenseTransactionDTO addExpenseDTO) : IRequest<Guid>;
}
