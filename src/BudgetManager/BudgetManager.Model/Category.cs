using System.ComponentModel.DataAnnotations;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.Models.MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Categories")]
    public class Category : ModelBase
    {
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