using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Wallets")]
    public class Wallet : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public DateTime DateOfChange { get; set; }
    }
}
