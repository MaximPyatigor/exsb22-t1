using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetTransactionListHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TransactionResponse>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<TransactionResponse>>(await _dataAccess.GetListByUserIdAsync(request.userId, cancellationToken));
            return result;
        }
    }
}