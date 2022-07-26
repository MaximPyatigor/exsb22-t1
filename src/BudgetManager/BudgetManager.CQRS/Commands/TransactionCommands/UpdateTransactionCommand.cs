using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTO;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record UpdateTransactionCommand(UpdateTransactionDTO transactionDTO) : IRequest<TransactionResponse>;
}
