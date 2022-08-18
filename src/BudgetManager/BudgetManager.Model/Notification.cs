using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.Models.MongoDB;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace BudgetManager.Model
{
    [CollectionName("Notifications")]
    public class Notification : IModelBase
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public NotificationTypes NotificationType { get; set; } = NotificationTypes.None;
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRead { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
