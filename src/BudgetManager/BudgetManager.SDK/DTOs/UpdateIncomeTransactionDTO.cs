namespace BudgetManager.SDK.DTOs
{
    public class UpdateIncomeTransactionDTO
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
