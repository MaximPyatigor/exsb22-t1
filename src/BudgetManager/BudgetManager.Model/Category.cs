using System.ComponentModel.DataAnnotations;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Categories")]
    public class Category : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Limit { get; set; }
        [Required]
        public LimitPeriods LimitPeriod { get; set; }
        public List<Category>? SubCategories { get; set; }
        [Required]
        public OperationType CategoryType { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
