using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddManyUsersHandler : IRequestHandler<AddManyUsersCommand, IEnumerable<Guid>>
    {
        private readonly IBaseRepository<DefaultCategory> _defaultCategoryRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddManyUsersHandler(IBaseRepository<DefaultCategory> defaultCategoryRepository, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _defaultCategoryRepository = defaultCategoryRepository ?? throw new ArgumentNullException(nameof(defaultCategoryRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyUsersCommand request, CancellationToken cancellationToken)
        {
            var mappedUsers = _mapper.Map<IEnumerable<User>>(request.users);

            var allDefaultCategories = await _defaultCategoryRepository.GetAllAsync(cancellationToken);
            var mappedDefaultCategories = _mapper.Map<IEnumerable<Category>>(allDefaultCategories);

            foreach (var user in mappedUsers)
                user.Categories.AddRange(mappedDefaultCategories);

            await _userRepository.InsertManyAsync(mappedUsers, cancellationToken);
            var listOfIds = _mapper.Map<IEnumerable<UserResponse>>(mappedUsers)
                .Select(u => u.Id).ToList();

            return listOfIds;
        }
    }
}
