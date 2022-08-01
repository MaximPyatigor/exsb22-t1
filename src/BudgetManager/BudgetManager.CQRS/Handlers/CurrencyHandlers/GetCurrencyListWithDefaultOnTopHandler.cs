

using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class GetCurrencyListWithDefaultOnTopHandler : IRequestHandler<GetCurrencyListWithDefaultOnTopQuery, IEnumerable<CurrencyResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Currency> _dataAccess;
        private readonly IBaseRepository<User> _userAccess;

        public GetCurrencyListWithDefaultOnTopHandler(IMapper mapper, IBaseRepository<Currency> dataAccess, IBaseRepository<User> userAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
            _userAccess = userAccess;
        }

        public async Task<IEnumerable<CurrencyResponse>> Handle(GetCurrencyListWithDefaultOnTopQuery request, CancellationToken cancellationToken)
        {
            var result = new List<CurrencyResponse>();
            var user = await _userAccess.FindByIdAsync(request.userId, cancellationToken);

            if (user is not null && user.DefaultCurrency is not null)
            {
                var defaultCurrency = user.DefaultCurrency;
                result.Add(_mapper.Map<CurrencyResponse>(defaultCurrency));
            }

            var currencies = _mapper.Map<IEnumerable<CurrencyResponse>>(await _dataAccess.GetAllAsync(cancellationToken));

            result.AddRange(currencies);

            return result;
        }
    }
}
