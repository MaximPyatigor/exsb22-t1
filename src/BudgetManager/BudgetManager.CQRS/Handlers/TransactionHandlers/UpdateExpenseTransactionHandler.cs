using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class UpdateExpenseTransactionHandler
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _dataAccess;

        public UpdateExpenseTransactionHandler(ITransactionRepository dataAccess, IMapper mapper, IMediator mediator)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ExpenseTransactionResponse> Handle(UpdateExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(builder.Eq(t => t.Id, request.updateExpenseDto.Id),
                builder.Eq(t => t.UserId, request.userId),
                builder.Eq(t => t.TransactionType, Model.Enums.OperationType.Expense));

            var projection = Builders<Transaction>.Projection.Include(t => t.WalletId);
            var oldExpenseTransaction = (await _dataAccess.FilterBy<Transaction>(filter, projection, cancellationToken)).FirstOrDefault();
            if (oldExpenseTransaction == null) { throw new KeyNotFoundException("Transaction not found"); }
            bool walletChanged = oldExpenseTransaction.WalletId != request.updateExpenseDto.WalletId;

            var update = Builders<Transaction>.Update
                .Set(o => o.WalletId, request.updateExpenseDto.WalletId)
                .Set(o => o.CategoryId, request.updateExpenseDto.CategoryId)
                .Set(o => o.SubCategoryId, request.updateExpenseDto.SubCategoryId)
                .Set(o => o.Payer, request.updateExpenseDto.Payer)
                .Set(o => o.DateOfTransaction, request.updateExpenseDto.DateOfTransaction)
                .Set(o => o.Value, request.updateExpenseDto.Value)
                .Set(o => o.Description, request.updateExpenseDto.Description);

            var response = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);
            if (response == null) { throw new KeyNotFoundException("Transaction not found"); }

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, response.WalletId, DateTime.UtcNow), cancellationToken);
            if (walletChanged) { await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, oldExpenseTransaction.WalletId, DateTime.UtcNow), cancellationToken); }

            return _mapper.Map<ExpenseTransactionResponse>(response);
        }
    }
}
