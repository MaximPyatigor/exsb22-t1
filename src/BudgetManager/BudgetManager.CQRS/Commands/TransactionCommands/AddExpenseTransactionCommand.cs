using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddExpenseTransactionCommand(AddExpenseTransactionDTO addExpenseDTO) : IRequest<Guid>;
}
