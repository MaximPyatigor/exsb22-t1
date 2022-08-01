using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class GetCurrencyListHandler : IRequestHandler<GetCurrencyListQuery, IEnumerable<CurrencyResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _dataAccess;

        public GetCurrencyListHandler(IMapper mapper, ICurrencyRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<CurrencyResponse>> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<CurrencyResponse>>(await _dataAccess.GetAllAsync(cancellationToken));

            return result;
        }
    }
}