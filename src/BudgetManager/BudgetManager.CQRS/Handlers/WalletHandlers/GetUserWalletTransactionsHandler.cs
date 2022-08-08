using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;
using MongoDB.Driver;
using BudgetManager.CQRS.Responses.WalletResponses;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetUserWalletTransactionsHandler : IRequestHandler<GetUserWalletTransactionsQuery, WalletTransactionsResponse>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetUserWalletTransactionsHandler(IBaseRepository<User> userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<WalletTransactionsResponse> Handle(GetUserWalletTransactionsQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);
            Wallet? wallet = user?.Wallets?.Find(w => w.Id == request.walletId);

            var builder = Builders<Transaction>.Filter;
            var transactionFilter = builder.Eq(t => t.UserId, request.userId) & builder.Eq(t => t.WalletId, request.walletId);
            var userWalletTransactions = await _transactionRepository.FilterBy(transactionFilter, cancellationToken);

            WalletTransactionsResponse walletTransactions = new WalletTransactionsResponse
            {
                Wallet = wallet,
                IsDefaultWallet = wallet?.Id == request.walletId,
                Transactions = userWalletTransactions.OrderByDescending(t => t.DateOfTransaction).ToList(),
            };

            return walletTransactions;
        }
    }
}
