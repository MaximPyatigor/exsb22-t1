namespace BudgetManager.SDK.DTOs
{
    public class AddTransactionDTO
    {
        public Guid CategoryId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal Value { get; set; }
        public int TransactionType { get; set; }
        public string Description { get; set; }
    }
}
