using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.TransactionResponses
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public decimal Value { get; set; }

        public OperationType TransactionType { get; set; }

        public string Description { get; set; }
    }
}
