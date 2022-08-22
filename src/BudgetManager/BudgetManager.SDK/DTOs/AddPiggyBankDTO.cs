using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.SDK.DTOs
{
    public class AddPiggyBankDTO
    {
        public string Name { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Target { get; set; }
    }
}
