using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;

namespace BudgetManager.Model
{
    [CollectionName("Currencies")]
    public class Currency : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
