using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class GetCurrencyListHandler : IRequestHandler<GetCurrencyListQuery, IEnumerable<CurrencyResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Currency> _dataAccess;

        public GetCurrencyListHandler(IMapper mapper, IBaseRepository<Currency> dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<IEnumerable<CurrencyResponse>> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<CurrencyResponse>>(await _dataAccess.GetAllAsync(cancellationToken));

            return result;
        }
    }
}