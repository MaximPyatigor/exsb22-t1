using BudgetManager.SDK.Pagination;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class IncomePageResponse
    {
        public IEnumerable<IncomeTransactionResponse> Incomes { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
