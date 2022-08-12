using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetRecentTransactionsHandler : IRequestHandler<GetRecentTransactionsQuery, List<RecentTransactionsHomepageResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBaseRepository<User> _userRepository;

        public GetRecentTransactionsHandler(ITransactionRepository transactionRepository, IBaseRepository<User> userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public async Task<List<RecentTransactionsHomepageResponse>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var transactions = await _transactionRepository.GetListByUserIdAsync(user.Id, cancellationToken);
            var tenRecents = transactions.OrderByDescending(t => t.DateOfTransaction).Take(10);

            if (tenRecents is null)
            {
                throw new KeyNotFoundException("No transactions found");
            }

            var response = new List<RecentTransactionsHomepageResponse>();
            foreach (var transaction in tenRecents)
            {
                Wallet? wallet = user.Wallets?.FirstOrDefault(w => w.Id == transaction.WalletId);
                response.Add(new RecentTransactionsHomepageResponse
                {
                    TransactionId = transaction.Id,
                    TransactionDate = transaction.DateOfTransaction,
                    CategoryName = transaction.TransactionType.ToString(),
                    Amount = transaction.Value,
                    Wallet = wallet,
                });
            }

            return response;
        }
    }
}
