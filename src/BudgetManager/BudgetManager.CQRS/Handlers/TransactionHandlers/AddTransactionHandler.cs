using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, string>
    {
        private readonly IBaseRepository<Transaction> _dataAccess;

        public AddTransactionHandler(IBaseRepository<Transaction> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<string> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return null; }
            Transaction transaction = new Transaction()
            {
                CategoryId = new Guid(request.transactionDto.CategoryId),
                DateOfTransaction = request.transactionDto.DateOfTransaction,
                Value = request.transactionDto.Value,
                CategoryType = (CategoryTypes)request.transactionDto.CategoryType,
                Description = request.transactionDto.Description,
            };

            await _dataAccess.InsertOneAsync(transaction, cancellationToken);
            return transaction.Id.ToString();
        }
    }
}
