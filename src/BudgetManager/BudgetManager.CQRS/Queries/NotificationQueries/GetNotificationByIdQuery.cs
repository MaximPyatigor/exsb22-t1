using BudgetManager.CQRS.Responses.NotificationResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Queries.NotificationQueries
{
    public record GetNotificationByIdQuery(Guid Id) : IRequest<NotificationResponse>;
}
