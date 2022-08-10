namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class TransactionResponse
    {
        public Guid TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string CategoryName { get; set; }

        public decimal Amount { get; set; }

        public string WalletName { get; set; }
    }
}
