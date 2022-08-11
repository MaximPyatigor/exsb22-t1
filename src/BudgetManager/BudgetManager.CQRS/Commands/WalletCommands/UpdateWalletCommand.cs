using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record UpdateWalletCommand(Guid UserId, bool isDefault, UpdateWalletDTO WalletDTO) : IRequest<WalletResponse>;
}
