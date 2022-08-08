using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record AddIncomeTransactionCommand(Guid userId, AddIncomeTransactionDTO addIncomeDTO) : IRequest<Guid>;
}
