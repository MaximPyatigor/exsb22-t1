using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record DeleteTransactionCommand(Guid id) : IRequest<bool>;
}
