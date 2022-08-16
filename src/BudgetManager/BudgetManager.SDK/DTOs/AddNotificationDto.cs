using BudgetManager.Model.Enums;

namespace BudgetManager.SDK.DTOs
{
    public class AddNotificationDto
    {
        public NotificationTypes NotificationType { get; set; }
        public string Description { get; set; }
    }
}
