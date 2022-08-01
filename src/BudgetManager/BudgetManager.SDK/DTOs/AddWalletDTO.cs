using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class AddWalletDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public Currency Currency { get; set; }
        public bool SetDefault { get; set; }
    }
}
