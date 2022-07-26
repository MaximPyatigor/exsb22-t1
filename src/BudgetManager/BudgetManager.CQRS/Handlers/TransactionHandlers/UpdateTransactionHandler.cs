using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, TransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Transaction> _dataAccess;

        public UpdateTransactionHandler(IBaseRepository<Transaction> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<TransactionResponse> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var existingTransactionObject = await _dataAccess.FindByIdAsync(request.transactionDTO.Id, cancellationToken);

            if (existingTransactionObject == null)
            {
                return null;
            }

            var mappedTransaction = _mapper.Map<Transaction>(request.transactionDTO);
            var response = await _dataAccess.ReplaceOneAsync(mappedTransaction, cancellationToken);

            return _mapper.Map<TransactionResponse>(response);
        }
    }
}
