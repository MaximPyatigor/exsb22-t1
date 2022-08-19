using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record UpdateWalletDateOfChangeCommand(Guid userId, Guid walletId, DateTimeOffset changeDate) : IRequest<Guid>;
}
