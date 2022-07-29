using BudgetManager.Model;
using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public LimitPeriods LimitPeriod { get; set; }
        public List<Category>? SubCategories { get; set; }
        public OperationType CategoryType { get; set; }
        public string Color { get; set; }
    }
}
