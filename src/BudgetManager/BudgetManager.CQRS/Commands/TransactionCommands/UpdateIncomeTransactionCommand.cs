using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;


namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record UpdateIncomeTransactionCommand(Guid userId, UpdateIncomeTransactionDTO updateIncomeDTO) : IRequest<IncomeTransactionResponse>;
}
