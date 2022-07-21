using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Commands.NotificationCommands
{
    public record DeleteNotificationCommand(Guid Id) : IRequest<bool>;
}
