using AutoMapper;
using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.CountryHandlers
{
    public class AddManyCountriesHandler : IRequestHandler<AddManyCountriesCommand, IEnumerable<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _dataAccess;

        public AddManyCountriesHandler(IMapper mapper, ICountryRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyCountriesCommand request, CancellationToken cancellationToken)
        {
            var counries = request.countries;
            await _dataAccess.InsertManyAsync(counries, cancellationToken);

            return counries.Select(c => c.Id);
        }
    }
}
