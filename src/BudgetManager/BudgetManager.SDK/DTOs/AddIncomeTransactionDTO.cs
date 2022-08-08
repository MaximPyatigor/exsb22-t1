using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddIncomeTransactionDTO
    {
        public Guid UserId { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime DateOfTransaction { get; set; }
        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
