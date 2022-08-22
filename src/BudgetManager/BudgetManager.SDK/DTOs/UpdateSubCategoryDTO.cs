namespace BudgetManager.SDK.DTOs
{
    public class UpdateSubCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
