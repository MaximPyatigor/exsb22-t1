using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;

        public DeleteUserHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteByIdAsync(request.id, cancellationToken);
            return result;
        }
    }
}
