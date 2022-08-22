using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddIncomeTransactionDTO
    {
        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTimeOffset DateOfTransaction { get; set; } = DateTimeOffset.Now;
        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
