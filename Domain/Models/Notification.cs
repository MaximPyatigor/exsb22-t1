using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Notification
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public NotificationTypes NotificationType { get; set; }
    }
}