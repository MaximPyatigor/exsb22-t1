using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, Guid>
    {
        private readonly IBaseRepository<Transaction> _dataAccess;

        public AddTransactionHandler(IBaseRepository<Transaction> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Guid> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = new Transaction()
            {
                CategoryId = new Guid(request.transactionDto.CategoryId),
                DateOfTransaction = request.transactionDto.DateOfTransaction,
                Value = request.transactionDto.Value,
                CategoryType = (CategoryTypes)request.transactionDto.CategoryType,
                Description = request.transactionDto.Description,
            };

            await _dataAccess.InsertOneAsync(transaction, cancellationToken);
            return transaction.Id;
        }
    }
}
