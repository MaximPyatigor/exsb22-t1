using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteIncomeTransactionHandler : IRequestHandler<DeleteIncomeTransactionCommand, bool>
    {
        private readonly IBaseRepository<Transaction> _transactionRepository;

        public DeleteIncomeTransactionHandler(IBaseRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(builder.Eq(t => t.Id, request.transactionId), builder.Eq(t => t.UserId, request.userId));
            bool result = await _transactionRepository.DeleteOneAsync(filter, cancellationToken);

            return result;
        }
    }
}
