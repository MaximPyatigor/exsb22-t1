using BudgetManager.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Responses.NotificationResponses
{
    public record NotificationResponse(string Id, NotificationTypes NotificationType, string Description, bool IsRead);
}
