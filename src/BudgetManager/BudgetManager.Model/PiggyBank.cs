using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Model
{
    public class PiggyBank
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Target { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Balance { get; set; }
        public bool IsSpent { get; set; }
    }
}
