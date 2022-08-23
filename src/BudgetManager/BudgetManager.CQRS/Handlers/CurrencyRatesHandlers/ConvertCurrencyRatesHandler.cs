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
    public class ConvertCurrencyRatesHandler : IRequestHandler<ConvertCurrencyRatesQuery, decimal>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<CurrencyRates> _dataAccess;
        private readonly IMediator _mediator;

        public ConvertCurrencyRatesHandler(IMapper mapper, IBaseRepository<CurrencyRates> dataAccess, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<decimal> Handle(ConvertCurrencyRatesQuery request, CancellationToken cancellationToken)
        {
            var currencyDict = (await _mediator.Send(new GetCurrencyRatesQuery())).Eur;

            var convertFrom = request.ConvertFrom.ToLower();
            var convertTo = request.ConvertTo.ToLower();

            decimal convertFromRate;
            decimal convertToRate;

            if (!currencyDict.TryGetValue(convertFrom, out convertFromRate))
            {
                throw new KeyNotFoundException($"Key {convertFrom} not found");
            }
            if (!currencyDict.TryGetValue(convertTo, out convertToRate))
            {
                throw new KeyNotFoundException($"Key {convertTo} not found");
            }

            var result = (convertToRate / convertFromRate) * request.Amount;
            return result;
        }
    }
}
