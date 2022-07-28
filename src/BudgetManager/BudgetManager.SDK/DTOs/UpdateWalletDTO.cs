using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class UpdateWalletDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public DateTime DateOfChange { get; set; }
    }
}
