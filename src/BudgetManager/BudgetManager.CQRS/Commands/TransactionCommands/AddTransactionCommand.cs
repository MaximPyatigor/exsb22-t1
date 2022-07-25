using BudgetManager.SDK;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddTransactionCommand(AddTransactionDto transactionDto) : IRequest<string>;
}
