namespace BudgetManager.SDK.DTOs
{
    public class WalletRecentTransactionsPageDTO
    {
        public Guid WalletId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
