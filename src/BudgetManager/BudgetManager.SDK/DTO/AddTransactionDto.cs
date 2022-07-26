namespace BudgetManager.SDK.DTO
{
    public class AddTransactionDto
    {
        public string CategoryId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal Value { get; set; }
        public int CategoryType { get; set; }
        public string Description { get; set; }
    }
}
