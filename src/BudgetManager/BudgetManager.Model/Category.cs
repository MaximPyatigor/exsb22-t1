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
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal? Limit { get; set; }
        public LimitPeriods LimitPeriod { get; set; } = LimitPeriods.None;
        public List<Category>? SubCategories { get; set; }
        public OperationType CategoryType { get; set; } = OperationType.None;
        public string Color { get; set; }
    }
}
