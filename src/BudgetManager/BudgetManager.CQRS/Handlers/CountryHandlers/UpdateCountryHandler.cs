using AutoMapper;
using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.CountryHandlers
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, CountryResponse>
    {
        private readonly IBaseRepository<Currency> _currencyRepository;
        private readonly IBaseRepository<Country> _countryRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateCountryHandler(IBaseRepository<Currency> currencyRepository, IBaseRepository<Country> countryRepository, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CountryResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.FindByIdAsync(request.updateCountryDTO.CountryId, cancellationToken);
            if (country == null) { throw new KeyNotFoundException("Country not found"); }

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var update = Builders<User>.Update.Set(u => u.Country, country);

            var currencyFilter = Builders<Currency>.Filter.Eq(c => c.CurrencyCode, country.CurrencyCode);
            var currency = (await _currencyRepository.FilterBy(currencyFilter, cancellationToken)).FirstOrDefault();
            if (currency == null) { throw new KeyNotFoundException("Currency not found"); }
            update = update.Set(u => u.DefaultCurrency, currency);

            var updatedUser = await _userRepository.UpdateOneAsync(filter, update, cancellationToken);
            if (updatedUser == null) { throw new KeyNotFoundException("User not found"); }

            return _mapper.Map<CountryResponse>(country);
        }
    }
}
