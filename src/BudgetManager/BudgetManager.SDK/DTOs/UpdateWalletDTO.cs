namespace BudgetManager.SDK.DTOs
{
    public class UpdateWalletDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CurrencyId { get; set; }
    }
}
