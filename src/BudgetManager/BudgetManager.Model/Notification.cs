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
        public Guid Id { get; set; }
        [Required]
        public NotificationTypes NotificationType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}
