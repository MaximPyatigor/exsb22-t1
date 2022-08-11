using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class AddWalletDTO
    {
        public string Name { get; set; }

        public Guid CurrencyId { get; set; }

        public bool SetDefault { get; set; }
    }
}
