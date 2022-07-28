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
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddManyUsersHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> Handle(AddManyUsersCommand request, CancellationToken cancellationToken)
        {
            var mappedUsers = _mapper.Map<IEnumerable<User>>(request.users);
            await _userRepository.InsertManyAsync(mappedUsers, cancellationToken);
            var listOfIds = _mapper.Map<IEnumerable<UserResponse>>(mappedUsers)
                .Select(u => u.Id).ToList();

            return listOfIds;
        }
    }
}
