using AutoMapper;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponse>
    {
        private readonly IBaseRepository<Transaction> _dataAccess;
        private readonly IMapper _mapper;

        public GetTransactionByIdHandler(IBaseRepository<Transaction> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<TransactionResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _dataAccess.FindByIdAsync(request.id, cancellationToken);
            return transaction == null ? null : _mapper.Map<TransactionResponse>(transaction);
        }
    }
}
