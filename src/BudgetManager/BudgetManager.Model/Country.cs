using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Countries")]
    public class Country : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
    }
}
