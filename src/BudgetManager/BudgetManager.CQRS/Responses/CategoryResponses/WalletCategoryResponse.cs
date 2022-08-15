using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.CategoryResponses
{
    public class WalletCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public OperationType CategoryType { get; set; }
    }
}
