using AutoMapper;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CurrencyHandlers
{
    public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Currency> _dataAccess;

        public GetCurrencyByIdHandler(IMapper mapper, IBaseRepository<Currency> dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dataAccess.FindByIdAsync(request.Id, cancellationToken);
            if (result == null)
            {
                throw new KeyNotFoundException("CurrencyId does not exist");
            }
            return result;
        }
    }
}