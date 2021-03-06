using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, bool>
    {
        private readonly IBaseRepository<Wallet> _dataAccess;

        public DeleteWalletHandler(IBaseRepository<Wallet> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var result = await _dataAccess.DeleteByIdAsync(request.id, cancellationToken);

            return result;
        }
    }
}
