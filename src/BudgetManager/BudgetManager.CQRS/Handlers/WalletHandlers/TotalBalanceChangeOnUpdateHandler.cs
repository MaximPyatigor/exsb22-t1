using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class TotalBalanceChangeOnUpdateHandler : IRequestHandler<ChangeTotalBalanceOfWalletOnUpdate, bool>
    {
        private readonly IBaseRepository<User> _userRepository;

        public TotalBalanceChangeOnUpdateHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeTotalBalanceOfWalletOnUpdate request, CancellationToken cancellationToken)
        {
            var transactionObject = request.transactionAfter;
            var transactionBefore = request.transactionBefore;
            var filterDefinition = Builders<User>.Filter.Eq(u => u.Id, request.transactionAfter.UserId)
                & Builders<User>.Filter.ElemMatch(u => u.Wallets, w => w.Id == transactionObject.WalletId);
            var projectionDefinition = Builders<User>.Projection.Exclude(u => u.Id).Include(u => u.Wallets[-1]);

            var user = (await _userRepository.FilterByAsync<User>(filterDefinition, projectionDefinition, cancellationToken)).FirstOrDefault();
            if (user == null) { throw new KeyNotFoundException("user not found"); }

            var wallet = user.Wallets.FirstOrDefault();
            if (wallet == null) { throw new KeyNotFoundException("wallet not found"); }

            switch (transactionObject.TransactionType)
            {
                case OperationType.Income:
                    wallet.Balance = wallet.Balance - transactionBefore.Value + transactionObject.Value;
                    break;
                case OperationType.Expense:
                    wallet.Balance = wallet.Balance + transactionBefore.Value - transactionObject.Value;
                    break;
            }

            var updateDefinition = Builders<User>.Update
                .Set(u => u.Wallets[-1].Balance, wallet.Balance);

            var result = await _userRepository.UpdateOneAsync(filterDefinition, updateDefinition, cancellationToken);

            return result is not null;
        }
    }
}
