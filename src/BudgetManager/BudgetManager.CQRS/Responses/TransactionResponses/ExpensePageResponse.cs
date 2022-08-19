using BudgetManager.SDK.Pagination;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class ExpensePageResponse
    {
        public IEnumerable<ExpenseTransactionResponse> Expenses { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
