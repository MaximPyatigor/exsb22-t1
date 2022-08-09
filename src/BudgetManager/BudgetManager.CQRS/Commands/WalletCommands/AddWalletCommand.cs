using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record AddWalletCommand(Guid userId, AddWalletDTO addWalletDTO) : IRequest<Guid>;
}
