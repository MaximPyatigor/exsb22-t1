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
        [JsonProperty("code")]
        public string CurrencyCode { get; set; }
        [Required]
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty("name_plural")]
        public string NamePlural { get; set; }
    }
}
