using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.SDK.DTOs
{
    public class AddPiggyBankDTO
    {
        public string Name { get; set; }
        public Guid CurrencyId { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Target { get; set; }
    }
}
