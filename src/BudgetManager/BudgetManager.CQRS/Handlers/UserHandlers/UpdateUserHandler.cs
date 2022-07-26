using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = request.updateUser;
            var existingUser = await _userRepository.FindByIdAsync(updateUser.Id, cancellationToken);
            if (existingUser is null)
            {
                return null;
            }

            var mappedUser = _mapper.Map<User>(updateUser);
            var response = await _userRepository.ReplaceOneAsync(mappedUser, cancellationToken);

            return response;
        }
    }
}
