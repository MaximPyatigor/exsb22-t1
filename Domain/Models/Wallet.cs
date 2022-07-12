using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Wallet
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public string Name { get; set; }
    }
}