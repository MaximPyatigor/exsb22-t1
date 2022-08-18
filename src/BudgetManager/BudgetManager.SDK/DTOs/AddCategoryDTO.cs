using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddCategoryDTO
    {
        public string Name { get; set; }

        public decimal Limit { get; set; }

        public LimitPeriods? LimitPeriod { get; set; }

        public OperationType CategoryType { get; set; }

        public string Color
        {
            get => CategoryType == OperationType.Income ? "green.500" : "red.500";
            set => Color = value;
        }
    }
}
