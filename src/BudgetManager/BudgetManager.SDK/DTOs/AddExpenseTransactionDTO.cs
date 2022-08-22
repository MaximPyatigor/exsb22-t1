namespace BudgetManager.SDK.DTOs
{
    public class AddExpenseTransactionDTO
    {
        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public string Payer { get; set; }

        public DateTimeOffset DateOfTransaction { get; set; } = DateTimeOffset.Now;

        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
