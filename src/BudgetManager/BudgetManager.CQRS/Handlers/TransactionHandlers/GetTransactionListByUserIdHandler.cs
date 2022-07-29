using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionListByUserIdHandler
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetTransactionListByUserIdHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TransactionResponse>> Handle(GetTransactionListByUserIdQuery request, CancellationToken cancellationToken)
        {

            var result = _mapper.Map<IEnumerable<TransactionResponse>>(await _dataAccess.GetListByUserIdAsync(request.userId, request.operationType, cancellationToken));
            return result;
        }
    }
}
