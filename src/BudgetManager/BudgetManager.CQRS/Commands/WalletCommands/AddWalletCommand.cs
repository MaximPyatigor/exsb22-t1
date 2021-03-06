using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record AddWalletCommand(AddWalletDTO walletDTO) : IRequest<Guid>;
}
