using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddSubCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public LimitPeriods LimitPeriod { get; set; }

        public OperationType CategoryType { get; set; } = OperationType.Expense;
        public string Color { get; set; }
    }
}
