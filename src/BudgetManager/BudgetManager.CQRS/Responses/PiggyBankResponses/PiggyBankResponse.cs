using BudgetManager.Model;

namespace BudgetManager.CQRS.Responses.PiggyBankResponses
{
    public class PiggyBankResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public decimal Target { get; set; }

        public decimal Balance { get; set; }

        public bool IsSpent { get; set; }
    }
}
