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
    public class UpdateIncomeTransactionHandler : IRequestHandler<UpdateIncomeTransactionCommand, IncomeTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public UpdateIncomeTransactionHandler(IMapper mapper, IMediator mediator, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _transactionRepository = transactionRepository;
        }

        public async Task<IncomeTransactionResponse> Handle(UpdateIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactionFilter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.Eq(t => t.UserId, request.userId),
                Builders<Transaction>.Filter.Eq(t => t.Id, request.updateIncomeDTO.Id),
                Builders<Transaction>.Filter.Eq(t => t.TransactionType, Model.Enums.OperationType.Income));

            var oldIncomeTransaction = (await _transactionRepository.FilterBy(transactionFilter, cancellationToken))
                                .FirstOrDefault() ?? throw new KeyNotFoundException("Transaction not found.");
            bool walletChanged = oldIncomeTransaction.WalletId != request.updateIncomeDTO.WalletId;

            var update = Builders<Transaction>.Update
                .Set(t => t.WalletId, request.updateIncomeDTO.WalletId)
                .Set(t => t.CategoryId, request.updateIncomeDTO.CategoryId)
                .Set(t => t.DateOfTransaction, request.updateIncomeDTO.DateOfTransaction)
                .Set(t => t.Value, request.updateIncomeDTO.Value)
                .Set(t => t.Description, request.updateIncomeDTO.Description);

            var updatedIncomeTransaction = await _transactionRepository.UpdateOneAsync(transactionFilter, update, cancellationToken) ?? throw new KeyNotFoundException("Transaction not found"); ;

            if (walletChanged)
            {
                await _mediator.Send(new ChangeTotalBalanceOfWalletOnDeleteCommand(oldIncomeTransaction), cancellationToken);
                await _mediator.Send(new ChangeTotalBalanceOfWalletCommand(updatedIncomeTransaction), cancellationToken);
                await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, oldIncomeTransaction.WalletId, DateTime.UtcNow), cancellationToken);
            }
            else
            {
                await _mediator.Send(new ChangeTotalBalanceOfWalletOnUpdate(oldIncomeTransaction, updatedIncomeTransaction), cancellationToken);
            }

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, updatedIncomeTransaction.WalletId, DateTime.UtcNow), cancellationToken);

            return _mapper.Map<IncomeTransactionResponse>(updatedIncomeTransaction);
        }
    }
}