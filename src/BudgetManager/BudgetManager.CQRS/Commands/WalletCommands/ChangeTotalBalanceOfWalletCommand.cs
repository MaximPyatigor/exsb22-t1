using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record ChangeTotalBalanceOfWalletCommand(Transaction transaction) : IRequest<bool>;
}
