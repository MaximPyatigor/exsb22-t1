using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Limit { get; set; }
        [Required]
        public LimitPeriods LimitPeriod { get; set; }
        [Required]
        public List<Guid> SubCategories { get; set; }
        [Required]
        public CategoryTypes CategoryType { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
