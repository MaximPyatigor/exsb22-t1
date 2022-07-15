using BudgetManager.Shared.Models.MongoDB.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{    
    public class Category
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Limit { get; set; }
        [Required]
        public LimitPeriods LimitPeriod { get; set; }
        public List<Guid>? SubCategories { get; set; }
        [Required]
        public CategoryTypes CategoryType { get; set; }
        [Required]
        public string Color { get; set; }
    }
}