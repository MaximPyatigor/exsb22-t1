using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Transaction> _dataAccess;

        public AddTransactionHandler(IBaseRepository<Transaction> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<Transaction>(request.transactionDto);
            await _dataAccess.InsertOneAsync(transaction, cancellationToken);
            return transaction.Id;
        }
    }
}
