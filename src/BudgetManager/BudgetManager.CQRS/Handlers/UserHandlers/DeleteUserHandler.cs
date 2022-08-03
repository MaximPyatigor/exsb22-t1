using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DeleteUserHandler(IBaseRepository<User> userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.DeleteByIdAsync(request.id, cancellationToken);
            var transactionResult = await _transactionRepository.DeleteManyByUserIdAsync(request.id, cancellationToken);
            return userResult && transactionResult;
        }
    }
}
