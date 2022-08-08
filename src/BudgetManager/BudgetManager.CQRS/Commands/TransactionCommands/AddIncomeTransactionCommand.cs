using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddIncomeTransactionCommand(AddIncomeTransactionDTO addIncomeDTO) : IRequest<Guid>;
}
