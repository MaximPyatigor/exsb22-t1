using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Wallets")]
    public class Wallet : ModelBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}