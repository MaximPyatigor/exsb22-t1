using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Transaction> _dataAccess;

        public GetTransactionListHandler(IMapper mapper, IBaseRepository<Transaction> dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TransactionResponse>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<TransactionResponse>>(await _dataAccess.GetAllAsync(cancellationToken));
            return result;
        }
    }
}