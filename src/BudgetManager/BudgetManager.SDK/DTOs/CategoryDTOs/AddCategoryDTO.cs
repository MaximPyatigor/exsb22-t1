using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs.CategoryDTOs
{
    public class AddCategoryDTO
    {
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public LimitPeriods LimitPeriod { get; set; }
        public List<Guid>? SubCategories { get; set; }
        public CategoryTypes CategoryType { get; set; }
        public string Color { get; set; }
    }
}
