using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Model
{
    public class PiggyBank : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Currency Currency { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Target { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Balance { get; set; }
        public bool IsSpent { get; set; }
    }
}
