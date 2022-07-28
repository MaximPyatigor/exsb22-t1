using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Currencies")]
    public class Currency : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
    }
}
