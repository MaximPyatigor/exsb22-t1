using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteIncomeTransactionHandler : IRequestHandler<DeleteIncomeTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteIncomeTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(
                builder.Eq(t => t.UserId, request.userId),
                builder.Eq(t => t.Id, request.transactionId),
                builder.Eq(t => t.TransactionType, Model.Enums.OperationType.Income));
            bool result = await _transactionRepository.DeleteOneAsync(filter, cancellationToken);

            return result;
        }
    }
}
