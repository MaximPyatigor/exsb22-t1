using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model.Enums;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetExpenseTransactionListHandler : IRequestHandler<GetExpenseTransactionListQuery, IEnumerable<ExpenseTransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetExpenseTransactionListHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<ExpenseTransactionResponse>> Handle(GetExpenseTransactionListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<ExpenseTransactionResponse>>(await _dataAccess.GetListByOperationAsync(request.userId, OperationType.Expense, cancellationToken));
            return result;
        }
    }
}
