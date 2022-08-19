using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class RecentTransactionResponse
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public DateTimeOffset DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public OperationType TransactionType { get; set; }
    }
}
