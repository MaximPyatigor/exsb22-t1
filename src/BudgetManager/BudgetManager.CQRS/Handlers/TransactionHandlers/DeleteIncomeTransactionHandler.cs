using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class DeleteIncomeTransactionHandler : IRequestHandler<DeleteIncomeTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMediator _mediator;

        public DeleteIncomeTransactionHandler(ITransactionRepository transactionRepository,
            IMediator mediator)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(DeleteIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(
                builder.Eq(t => t.UserId, request.userId),
                builder.Eq(t => t.Id, request.transactionId),
                builder.Eq(t => t.TransactionType, Model.Enums.OperationType.Income));
            var transaction = (await _transactionRepository.FilterByAsync(filter, cancellationToken)).FirstOrDefault();

            await _mediator.Send(new ChangeTotalBalanceOfWalletOnDeleteCommand(transaction), cancellationToken);
            bool result = await _transactionRepository.DeleteOneAsync(filter, cancellationToken);
            return result;
        }
    }
}
