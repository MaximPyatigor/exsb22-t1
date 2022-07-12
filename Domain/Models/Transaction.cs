using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
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