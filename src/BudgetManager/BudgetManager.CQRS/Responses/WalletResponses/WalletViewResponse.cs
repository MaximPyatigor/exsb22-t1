using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.WalletResponses
{
    public class WalletViewResponse
    {
        public Guid WalletId { get; set; }

        public string WalletName { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}
