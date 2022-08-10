using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class UpdateUserDTO
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public Currency DefaultCurrency { get; set; }
        public Guid DefaultWallet { get; set; }
        public Country Country { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Wallet>? Wallets { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<string>? Payers { get; set; }
    }
}
