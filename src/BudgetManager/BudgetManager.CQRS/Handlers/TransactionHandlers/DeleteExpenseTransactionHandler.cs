using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteExpenseTransactionHandler : IRequestHandler<DeleteExpenseTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteExpenseTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(
                builder.Eq(t => t.UserId, request.userId),
                builder.Eq(t => t.Id, request.expenseId),
                builder.Eq(t => t.TransactionType, Model.Enums.OperationType.Expense));
            bool result = await _transactionRepository.DeleteOneAsync(filter, cancellationToken);

            return result;
        }
    }
}
