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
        private readonly ITransactionRepository _dataAccess;

        public UpdateIncomeTransactionHandler(IMapper mapper, IMediator mediator, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _mediator = mediator;
            _dataAccess = dataAccess;
        }

        public async Task<IncomeTransactionResponse> Handle(UpdateIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.Eq(t => t.UserId, request.userId),
                Builders<Transaction>.Filter.Eq(t => t.Id, request.updateIncomeDTO.Id),
                Builders<Transaction>.Filter.Eq(t => t.TransactionType, Model.Enums.OperationType.Income));

            var update = Builders<Transaction>.Update
                .Set(t => t.WalletId, request.updateIncomeDTO.WalletId)
                .Set(t => t.CategoryId, request.updateIncomeDTO.CategoryId)
                .Set(t => t.DateOfTransaction, request.updateIncomeDTO.DateOfTransaction)
                .Set(t => t.Value, request.updateIncomeDTO.Value)
                .Set(t => t.Description, request.updateIncomeDTO.Description);

            var updatedIncomeTransaction = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);
            if (updatedIncomeTransaction == null) { throw new KeyNotFoundException("Transaction not found"); }

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(request.userId, request.updateIncomeDTO.WalletId, DateTime.UtcNow), cancellationToken);

            return _mapper.Map<IncomeTransactionResponse>(updatedIncomeTransaction);
        }
    }
}