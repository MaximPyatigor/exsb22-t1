using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record ChangeTotalBalanceOfWalletOnDeleteCommand(Transaction transaction) : IRequest<bool>;
}
