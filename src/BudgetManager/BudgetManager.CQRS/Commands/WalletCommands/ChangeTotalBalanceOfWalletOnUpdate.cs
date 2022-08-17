using MediatR;
using BudgetManager.Model;

namespace BudgetManager.CQRS.Commands.WalletCommands
{
    public record ChangeTotalBalanceOfWalletOnUpdate(Transaction transactionBefore, Transaction transactionAfter) : IRequest<bool>;
}
