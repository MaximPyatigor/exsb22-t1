namespace BudgetManager.CQRS.Responses.WalletResponses
{
    public class WalletResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal Balance { get; set; }
    }
}
