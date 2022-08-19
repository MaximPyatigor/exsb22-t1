using BudgetManager.Model.Enums;

namespace BudgetManager.CQRS.Responses.NotificationResponses
{
    public class NotificationResponse 
    {
        public Guid Id { get; set; }

        public NotificationTypes NotificationType { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Date { get; set; }

        public bool IsRead { get; set; }
    }
}
