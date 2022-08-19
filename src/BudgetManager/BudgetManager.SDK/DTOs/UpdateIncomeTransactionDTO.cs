namespace BudgetManager.SDK.DTOs
{
    public class UpdateIncomeTransactionDTO
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTimeOffset DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
