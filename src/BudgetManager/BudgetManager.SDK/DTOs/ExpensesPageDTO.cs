namespace BudgetManager.SDK.DTOs
{
    public class ExpensesPageDTO
    {
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public IEnumerable<Guid>? CategoriesFilter { get; set; }
        public IEnumerable<Guid>? WalletsFilter { get; set; }
        public IEnumerable<string>? PayersFilter { get; set; }
        public bool IsSortByDate { get; set; } = true;
        public bool IsSortByAmount { get; set; }
        public bool IsSortDescending { get; set; } = true;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
