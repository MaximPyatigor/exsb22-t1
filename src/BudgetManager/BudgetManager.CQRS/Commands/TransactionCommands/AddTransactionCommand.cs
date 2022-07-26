using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddTransactionCommand(AddTransactionDto transactionDto) : IRequest<string>;
}
