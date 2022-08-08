using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record DeleteUserWalletCommand(Guid userId, Guid walletId) : IRequest<bool>;
}
