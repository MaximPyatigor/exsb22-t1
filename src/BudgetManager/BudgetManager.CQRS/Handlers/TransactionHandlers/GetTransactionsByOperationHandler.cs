using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionsByOperationHandler : IRequestHandler<GetTransactionListByOperationQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetTransactionsByOperationHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TransactionResponse>> Handle(GetTransactionListByOperationQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<TransactionResponse>>(await _dataAccess.GetListByOperationAsync(request.userId, request.operationType, cancellationToken));
            return result;
        }
    }
}