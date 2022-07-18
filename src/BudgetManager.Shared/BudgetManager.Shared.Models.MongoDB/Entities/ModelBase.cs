using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{
    public class ModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
