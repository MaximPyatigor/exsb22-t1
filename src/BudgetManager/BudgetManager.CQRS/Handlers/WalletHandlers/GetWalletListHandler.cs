using AutoMapper;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletListHandler : IRequestHandler<GetWalletListQuery, IEnumerable<WalletResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Wallet> _dataAccess;

        public GetWalletListHandler(IMapper mapper, IBaseRepository<Wallet> dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<WalletResponse>> Handle(GetWalletListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<WalletResponse>>(await _dataAccess.GetAllAsync(cancellationToken));
            return result;
        }
    }
}
