using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using MediatR;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.SDK.DTOs;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IBaseRepository<DefaultCategory> _defaultCategoryRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddUserHandler(IBaseRepository<DefaultCategory> defaultCategoryRepository, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _defaultCategoryRepository = defaultCategoryRepository;
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

            await _userRepository.InsertOneAsync(mappedUser, cancellationToken);
            return mappedUser.Id;
        }
    }
}
