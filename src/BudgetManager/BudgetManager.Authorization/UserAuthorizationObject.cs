using BudgetManager.Model;

namespace BudgetManager.Authorization
{
    public class UserAuthorizationObject
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string Email { get; set; }

        public Currency DefaultCurrency { get; set; }

        public Guid DefaultWallet { get; set; }

        public Country Country { get; set; }

        public bool IsAdmin { get; set; }
    }
}
