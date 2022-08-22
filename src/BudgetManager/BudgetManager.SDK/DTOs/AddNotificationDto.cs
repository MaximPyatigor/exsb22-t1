using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddNotificationDto
    {
        public NotificationTypes NotificationType { get; set; } = NotificationTypes.None;
        public string Description { get; set; }
    }
}
