using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, TransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public UpdateTransactionHandler(ITransactionRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<TransactionResponse> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var mappedTransaction = _mapper.Map<Transaction>(request.transactionDTO);
            var filter = Builders<Transaction>.Filter.Eq(opt => opt.Id, mappedTransaction.Id);
            var update = Builders<Transaction>.Update
                .Set(o => o.WalletId, mappedTransaction.WalletId)
                .Set(o => o.CategoryId, mappedTransaction.CategoryId)
                .Set(o => o.Payer, mappedTransaction.Payer)
                .Set(o => o.DateOfTransaction, mappedTransaction.DateOfTransaction)
                .Set(o => o.Value, mappedTransaction.Value)
                .Set(o => o.Description, mappedTransaction.Description);

            var response = _mapper.Map<TransactionResponse>(await _dataAccess.UpdateOneAsync(filter, update, cancellationToken));

            return _mapper.Map<TransactionResponse>(response);
        }
    }
}
