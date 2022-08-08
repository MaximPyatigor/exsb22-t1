using MediatR;

namespace BudgetManager.CQRS.Commands.TransactionCommands
{
    public record DeleteIncomeTransactionCommand(Guid userId, Guid transactionId) : IRequest<bool>;
}
