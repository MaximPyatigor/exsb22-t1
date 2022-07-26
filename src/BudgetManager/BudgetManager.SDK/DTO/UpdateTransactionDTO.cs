namespace BudgetManager.SDK.DTO
{
    public class UpdateTransactionDTO
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal Value { get; set; }
        public int CategoryType { get; set; }
        public string Description { get; set; }
    }
}
