using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyRatesQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.CurrencyRatesHandlers
{
    public class GetCurrencyRatesHandler : IRequestHandler<GetCurrencyRatesQuery, CurrencyRates>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<CurrencyRates> _dataAccess;

        public GetCurrencyRatesHandler(IMapper mapper, IBaseRepository<CurrencyRates> dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<CurrencyRates> Handle(GetCurrencyRatesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _dataAccess.GetAllAsync(CancellationToken.None)).FirstOrDefault();
            return result;
        }
    }
}
