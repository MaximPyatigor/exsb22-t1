using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{
    [CollectionName("Transactions")]
    public class Transaction
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public CategoryTypes CategoryType { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        [Required]
        public string Description { get; set; }
    }
}