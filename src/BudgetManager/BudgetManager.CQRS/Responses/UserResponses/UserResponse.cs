using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.UserResponses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public string DefaultCurrency { get; set; }

        public List<Category>? Categories { get; set; }

        public List<Wallet>? Wallets { get; set; }

        public List<Notification>? Notifications { get; set; }
    }
}
