using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _dataAccess;

        public DeleteTransactionHandler(ITransactionRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _dataAccess.DeleteByIdAsync(request.id, cancellationToken);
            return isDeleted;
        }
    }
}
