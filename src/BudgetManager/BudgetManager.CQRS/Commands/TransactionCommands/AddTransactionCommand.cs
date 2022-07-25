using BudgetManager.SDK.DTO;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddTransactionCommand(AddTransactionDto transactionDto) : IRequest<string>;
}
