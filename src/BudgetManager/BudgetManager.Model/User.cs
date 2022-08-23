using System.ComponentModel.DataAnnotations;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Users")]
    public class User : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        public Currency? DefaultCurrency { get; set; }
        public Guid? DefaultWallet { get; set; }
        public Country? Country { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Wallet>? Wallets { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<string>? Payers { get; set; }
        public List<PiggyBank>? PiggyBanks { get; set; }
    }
}
