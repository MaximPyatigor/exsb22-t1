using BudgetManager.Shared.Models.MongoDB.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Shared.Models.MongoDB.Entities
{
    public class Notification
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public NotificationTypes NotificationType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}