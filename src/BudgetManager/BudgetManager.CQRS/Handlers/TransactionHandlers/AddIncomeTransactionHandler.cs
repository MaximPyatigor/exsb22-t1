using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddIncomeTransactionHandler : IRequestHandler<AddIncomeTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public AddIncomeTransactionHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<Guid> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var incomeTransaction = _mapper.Map<Transaction>(request.addIncomeDTO);
            await _dataAccess.InsertOneAsync(incomeTransaction, cancellationToken);
            return incomeTransaction.Id;
        }
    }
}
