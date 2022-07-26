using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record DeleteWalletCommand(Guid id) : IRequest<bool>;
}
