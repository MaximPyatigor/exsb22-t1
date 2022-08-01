using AutoMapper;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CountryHandlers
{
    public class GetCountryListHandler : IRequestHandler<GetCountryListQuery, IEnumerable<CountryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _dataAccess;

        public GetCountryListHandler(IMapper mapper, ICountryRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<CountryResponse>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<CountryResponse>>(await _dataAccess.GetAllAsync(cancellationToken));

            return result;
        }
    }
}
