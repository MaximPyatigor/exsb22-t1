using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.PiggyBankCommands
{
    public record AddPiggyBankCommand(Guid userId, AddPiggyBankDTO AddPiggyBankDTO) : IRequest<Guid>;
}
