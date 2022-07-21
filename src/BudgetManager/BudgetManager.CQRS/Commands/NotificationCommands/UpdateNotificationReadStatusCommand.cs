using BudgetManager.CQRS.Responses.NotificationResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Commands.NotificationCommands
{
    public record UpdateNotificationReadStatusCommand(Guid Id, bool IsRead) : IRequest<NotificationResponse>;
}
