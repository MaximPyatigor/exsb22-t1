using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{
    [CollectionName("Users")]
    public class User
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DefaultCurrency { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Wallet>? Wallets { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
