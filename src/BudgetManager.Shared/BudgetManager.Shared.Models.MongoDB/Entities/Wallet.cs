using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{
    public class Wallet
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}