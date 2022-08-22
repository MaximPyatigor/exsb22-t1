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
    public class UpdateExpenseTransactionHandler : IRequestHandler<UpdateExpenseTransactionCommand, ExpenseTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public UpdateExpenseTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper, IMediator mediator)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ExpenseTransactionResponse> Handle(UpdateExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var builder = Builders<Transaction>.Filter;
            var transactionFilter = builder.And(builder.Eq(t => t.Id, request.updateExpenseDto.Id),
                builder.Eq(t => t.UserId, request.userId),
                builder.Eq(t => t.TransactionType, Model.Enums.OperationType.Expense));

            var oldExpenseTransaction = (await _transactionRepository.FilterByAsync(transactionFilter, cancellationToken))
                                .FirstOrDefault() ?? throw new KeyNotFoundException("Transaction not found.");
            bool walletChanged = oldExpenseTransaction.WalletId != request.updateExpenseDto.WalletId;

            var update = Builders<Transaction>.Update
                .Set(o => o.WalletId, request.updateExpenseDto.WalletId)
                .Set(o => o.CategoryId, request.updateExpenseDto.CategoryId)
                .Set(o => o.SubCategoryId, request.updateExpenseDto.SubCategoryId)
                .Set(o => o.Payer, request.updateExpenseDto.Payer)
                .Set(o => o.DateOfTransaction, request.updateExpenseDto.DateOfTransaction)
                .Set(o => o.Value, request.updateExpenseDto.Value)
                .Set(o => o.Description, request.updateExpenseDto.Description);

            var response = await _transactionRepository.UpdateOneAsync(transactionFilter, update, cancellationToken) ?? throw new KeyNotFoundException("Transaction not found");

            if (walletChanged)
            {
                await _mediator.Send(new ChangeTotalBalanceOfWalletOnDeleteCommand(oldExpenseTransaction), cancellationToken);
                await _mediator.Send(new ChangeTotalBalanceOfWalletCommand(response), cancellationToken);
                await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, oldExpenseTransaction.WalletId, DateTimeOffset.Now), cancellationToken);
            }
            else
            {
                await _mediator.Send(new ChangeTotalBalanceOfWalletOnUpdate(oldExpenseTransaction, response), cancellationToken);
            }

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, response.WalletId, DateTimeOffset.Now), cancellationToken);

            return _mapper.Map<ExpenseTransactionResponse>(response);
        }
    }
}
