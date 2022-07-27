using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Currencies")]
    public class Currency
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
    }
}
