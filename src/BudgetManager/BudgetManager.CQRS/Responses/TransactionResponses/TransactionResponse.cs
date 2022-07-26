using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public CategoryTypes CategoryType { get; set; }

        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }

        public string Description { get; set; }
    }
}
