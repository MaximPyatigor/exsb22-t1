using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddManyUsersHandler : IRequestHandler<AddManyUsersCommand, IEnumerable<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddManyUsersHandler(IUserRepository userRepository, IMapper mapper)
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
