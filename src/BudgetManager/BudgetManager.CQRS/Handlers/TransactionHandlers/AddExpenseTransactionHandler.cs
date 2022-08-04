using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddExpenseTransactionHandler : IRequestHandler<AddExpenseTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public AddExpenseTransactionHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<Guid> Handle(AddExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var expenseTransaction = _mapper.Map<Transaction>(request.addExpenseDTO);
            await _dataAccess.InsertOneAsync(expenseTransaction, cancellationToken);
            return expenseTransaction.Id;
        }
    }
}
