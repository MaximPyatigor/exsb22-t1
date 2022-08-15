using BudgetManager.Model;

namespace BudgetManager.CQRS.Projections.UserProjections
{
    public class UserNotificationListProjection
    {
        public IEnumerable<Notification>? Notifications { get; set; }
    }
}
