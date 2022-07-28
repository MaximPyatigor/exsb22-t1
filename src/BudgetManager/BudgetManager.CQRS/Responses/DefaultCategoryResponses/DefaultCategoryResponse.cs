using BudgetManager.Model;
using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.DefaultCategoryResponses
{
    public class DefaultCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Category>? SubCategories { get; set; }

        public OperationType CategoryType { get; set; }

        public string Color { get; set; }
    }
}
