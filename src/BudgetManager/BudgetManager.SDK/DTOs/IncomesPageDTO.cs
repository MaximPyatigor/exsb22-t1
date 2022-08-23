namespace BudgetManager.SDK.DTOs
{
    public class IncomesPageDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public IEnumerable<Guid>? CategoriesFilter { get; set; }
        public IEnumerable<Guid>? WalletsFilter { get; set; }
        public bool IsSortByDate { get; set; } = true;
        public bool IsSortByAmount { get; set; }
        public bool IsSortDescending { get; set; } = true;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
