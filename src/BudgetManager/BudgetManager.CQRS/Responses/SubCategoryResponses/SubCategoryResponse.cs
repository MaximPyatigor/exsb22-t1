using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.SubCategoryResponses
{
    public class SubCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Limit { get; set; }

        public LimitPeriods? LimitPeriod { get; set; }

        public OperationType CategoryType { get; set; }

        public string Color { get; set; }
    }
}
