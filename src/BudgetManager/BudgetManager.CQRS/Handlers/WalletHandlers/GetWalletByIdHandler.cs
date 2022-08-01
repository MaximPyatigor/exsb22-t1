using AutoMapper;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, WalletResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWalletRepository _dataAccess;

        public GetWalletByIdHandler(IMapper mapper, IWalletRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }


        public async Task<WalletResponse> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<WalletResponse>(await _dataAccess.FindByIdAsync(request.id, cancellationToken));

            return result;
        }
    }
}
