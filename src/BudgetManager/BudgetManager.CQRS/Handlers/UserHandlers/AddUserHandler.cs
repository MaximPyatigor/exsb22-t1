using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using MediatR;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.SDK.DTOs;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IBaseRepository<DefaultCategory> _defaultCategoryRepository;
        private readonly IBaseRepository<Currency> _currencyRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddUserHandler(IBaseRepository<DefaultCategory> defaultCategoryRepository, IBaseRepository<Currency> currencyRepository, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _defaultCategoryRepository = defaultCategoryRepository;
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            AddUserDTO requestUser = request.user;
            User mappedUser = _mapper.Map<User>(requestUser);

            var allDefaultCategories = await _defaultCategoryRepository.GetAllAsync(cancellationToken);
            var mappedDefaultCategories = _mapper.Map<IEnumerable<Category>>(allDefaultCategories);
            mappedUser.Categories.AddRange(mappedDefaultCategories);

            if (mappedUser.DefaultCurrency is null && mappedUser.Country is not null)
            {
                var filter = Builders<Currency>.Filter.Eq(c => c.CurrencyCode, mappedUser.Country.CurrencyCode);
                var response = await _currencyRepository.FilterBy(filter, cancellationToken);
                var currency = response.FirstOrDefault();

                if (currency is not null) { mappedUser.DefaultCurrency = currency; }
            }

            await _userRepository.InsertOneAsync(mappedUser, cancellationToken);
            return mappedUser.Id;
        }
    }
}
