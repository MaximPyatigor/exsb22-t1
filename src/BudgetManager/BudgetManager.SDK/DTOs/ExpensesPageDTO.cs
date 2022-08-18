namespace BudgetManager.SDK.DTOs
{
    public class ExpensesPageDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<Guid>? CategoriesFilter { get; set; }
        public List<Guid>? WalletsFilter { get; set; }
        public List<string>? PayersFilter { get; set; }
        public bool IsSortByDate { get; set; } = true;
        public bool IsSortByAmount { get; set; }
        public bool IsSortDescending { get; set; } = true;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
