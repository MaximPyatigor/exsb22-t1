using BudgetManager.Model;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Projections.UserProjections
{
    public class UserNotificationListProjection
    {
        public IEnumerable<Notification>? Notifications { get; set; }
    }
}
