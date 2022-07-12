using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public override string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string DefaultCurrency { get; set; }
        public List<Category> Categories { get; set; }
        public List<Wallet> Wallets { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}