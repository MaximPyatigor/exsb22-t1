using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionListByWalletHandler : IRequestHandler<GetTransactionListByWalletQuery, IEnumerable<Transaction>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _dataAccess;

        public GetTransactionListByWalletHandler(IMapper mapper, ITransactionRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionListByWalletQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<Transaction>>(await _dataAccess.GetListByWalletIdAsync(request.WalletId, cancellationToken));
            return result;
        }
    }
}
