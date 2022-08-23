using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class UpdateWalletDateOfChangeHandler : IRequestHandler<UpdateWalletDateOfChangeCommand, Guid>
    {
        private readonly IBaseRepository<User> _dataAccess;

        public UpdateWalletDateOfChangeHandler(IBaseRepository<User> dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Guid> Handle(UpdateWalletDateOfChangeCommand request, CancellationToken cancellationToken)
        {
            var result = await _dataAccess.UpdateOneAsync(
                u => u.Id == request.userId && u.Wallets.Any(w => w.Id == request.walletId),
                Builders<User>.Update.Set(w => w.Wallets[-1].DateOfChange, request.changeDate), cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("User or wallet is not found");
            }

            return result.Id;
        }
    }
}
