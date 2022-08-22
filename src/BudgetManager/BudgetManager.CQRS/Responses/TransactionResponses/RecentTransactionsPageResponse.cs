using BudgetManager.SDK.Pagination;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class RecentTransactionsPageResponse
    {
        public IEnumerable<RecentTransactionResponse> RecentTransactions { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
