using BudgetManager.CQRS.Pagination;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class IncomePageResponse
    {
        public IEnumerable<IncomeTransactionResponse> Incomes { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
