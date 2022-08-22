using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.UserResponses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public Currency DefaultCurrency { get; set; }

        public Guid DefaultWallet { get; set; }

        public Country Country { get; set; }

        public List<Category>? Categories { get; set; }

        public List<Wallet>? Wallets { get; set; }

        public List<Notification>? Notifications { get; set; }

        public List<string>? Payers { get; set; }
        public List<PiggyBank>? PiggyBanks { get; set; }

        public bool IsAdmin { get; set; }
    }
}
