using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class GetTop10RecentTransactionsHandler : IRequestHandler<GetTop10RecentTransactionsQuery, List<TransactionResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetTop10RecentTransactionsHandler(IBaseRepository<User> userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionResponse>> Handle(GetTop10RecentTransactionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var allTransactions = await _transactionRepository.GetListByUserIdAsync(user.Id, cancellationToken);
            var topTenRecentTransactions = allTransactions.OrderByDescending(t => t.DateOfTransaction).Take(10);

            var result = new List<TransactionResponse>();
            foreach (var transaction in topTenRecentTransactions)
            {
                string walletName = user.Wallets?.FirstOrDefault(w => w.Id == transaction.WalletId)?.Name ?? "Deleted wallet";
                result.Add(
                    new TransactionResponse
                    {
                        TransactionId = transaction.Id,
                        TransactionDate = transaction.DateOfTransaction,
                        CategoryName = transaction.TransactionType.ToString(),
                        Amount = transaction.Value,
                        WalletName = walletName,
                    });
            }

            return result;
        }
    }
}
