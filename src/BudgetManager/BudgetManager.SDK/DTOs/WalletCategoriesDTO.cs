using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class WalletCategoriesDTO
    {
        public Guid WalletId { get; set; }
        public OperationType CategoryType { get; set; }
    }
}
