using AutoMapper;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using MediatR;
using BudgetManager.SDK.DTOs;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            AddUserDTO requestUser = request.user;
            User mappedUser = _mapper.Map<User>(requestUser);
            await _userRepository.InsertOneAsync(mappedUser, cancellationToken);
            return mappedUser.Id;
        }
    }
}
