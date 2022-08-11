namespace BudgetManager.SDK.DTOs
{
    public class IncomesPageDTO
    {
        public DateTime? DateFilter { get; set; }
        public Guid CategoryIdFilter { get; set; } = Guid.Empty;
        public Guid WalletIdFilter { get; set; } = Guid.Empty;
        public bool IsSortByDate { get; set; } = true;
        public bool IsSortByAmount { get; set; }
        public bool IsSortDescending { get; set; } = true;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
