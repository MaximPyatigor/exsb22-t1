namespace BudgetManager.SDK.DTOs
{
    public class AddTransactionDTO
    {
        public string CategoryId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal Value { get; set; }
        public int CategoryType { get; set; }
        public string Description { get; set; }
    }
}
