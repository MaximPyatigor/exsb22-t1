using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyRatesQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyRatesHandlers
{
    public class GetCurrencyRatesHandler : IRequestHandler<GetCurrencyRatesQuery, CurrencyRates>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<CurrencyRates> _dataAccess;

        public GetCurrencyRatesHandler(IMapper mapper, IBaseRepository<CurrencyRates> dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<CurrencyRates> Handle(GetCurrencyRatesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _dataAccess.GetAllAsync(CancellationToken.None)).FirstOrDefault();
            return result;
        }
    }
}
