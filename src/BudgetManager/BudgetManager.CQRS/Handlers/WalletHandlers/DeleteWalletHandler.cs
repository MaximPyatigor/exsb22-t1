using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, bool>
    {
        private readonly IWalletRepository _dataAccess;

        public DeleteWalletHandler(IWalletRepository dataAccess)
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
