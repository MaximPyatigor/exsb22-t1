using System.ComponentModel.DataAnnotations;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Transactions")]
    public class Transaction : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid WalletId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string? Payer { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public OperationType TransactionType { get; set; }
        public string Description { get; set; }
    }
}
