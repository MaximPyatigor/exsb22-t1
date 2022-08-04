using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class UpdateWalletDateOfChangeHandler : IRequestHandler<UpdateWalletDateOfChangeCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;

        public UpdateWalletDateOfChangeHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateWalletDateOfChangeCommand request, CancellationToken cancellationToken)
        {
            //var builer = Builders<User>.Filter;
            //var walletFilter = builer.Eq(t => t.Id, request.userId) & builer.Eq(t => t.Wallets.FirstOrDefault, request.walletId);
            return Guid.NewGuid();
        }
    }
}
