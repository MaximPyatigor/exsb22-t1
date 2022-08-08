using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class UpdateExpenseTransactionHandler
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public UpdateExpenseTransactionHandler(ITransactionRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<ExpenseTransactionResponse> Handle(UpdateExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var mappedTransaction = _mapper.Map<Transaction>(request.updateExpenseDto);
            var builder = Builders<Transaction>.Filter;
            var filter = builder.And(builder.Eq(t => t.Id, mappedTransaction.Id),
                builder.Eq(t => t.UserId, request.userId));
            var update = Builders<Transaction>.Update
                .Set(o => o.WalletId, mappedTransaction.WalletId)
                .Set(o => o.CategoryId, mappedTransaction.CategoryId)
                .Set(o => o.Payer, mappedTransaction.Payer)
                .Set(o => o.DateOfTransaction, mappedTransaction.DateOfTransaction)
                .Set(o => o.Value, mappedTransaction.Value)
                .Set(o => o.Description, mappedTransaction.Description);

            var response = _mapper.Map<ExpenseTransactionResponse>(await _dataAccess.UpdateOneAsync(filter, update, cancellationToken));

            return _mapper.Map<ExpenseTransactionResponse>(response);
        }
    }
}
