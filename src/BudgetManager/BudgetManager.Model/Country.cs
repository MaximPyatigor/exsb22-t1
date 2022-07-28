using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;

namespace BudgetManager.Model
{
    [CollectionName("Countries")]
    public class Country : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [Required]
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }
}
