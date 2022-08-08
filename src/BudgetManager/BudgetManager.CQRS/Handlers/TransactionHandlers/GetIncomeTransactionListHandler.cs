using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model.Enums;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetIncomeTransactionListHandler : IRequestHandler<GetIncomeTransactionListQuery, IEnumerable<IncomeTransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetIncomeTransactionListHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<IncomeTransactionResponse>> Handle(GetIncomeTransactionListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<IncomeTransactionResponse>>(await _dataAccess.GetListByOperationAsync(request.userId, OperationType.Income, cancellationToken));
            return result;
        }
    }
}
