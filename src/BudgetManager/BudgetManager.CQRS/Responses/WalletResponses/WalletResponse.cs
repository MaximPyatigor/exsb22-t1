using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.WalletResponses
{
    public class WalletResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public DateTime DateOfChange { get; set; }
    }
}
