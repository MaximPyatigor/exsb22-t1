using System;
using System.Collections.Generic;
using System.Globalization;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BudgetManager.Model
{
    [CollectionName("CurrencyRates")]
    public class CurrencyRates : IModelBase
    {
        [JsonIgnore]
        [BsonId]
        public Guid Id { get; set; } = new Guid("66c05d55-dc7a-4d91-a965-a037db973b06");

        [JsonProperty("date")]
        public string Date { get; set; }

        // Eur is used as a base for currency rates
        [JsonProperty("eur")]
        public Dictionary<string, double> Eur { get; set; }
    }
}