using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, NotificationResponse>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public GetNotificationByIdHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<NotificationResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _dataAccess.FindByIdAsync(request.Id, cancellationToken);
            return notification == null ? null : new NotificationResponse(notification.Id, notification.NotificationType, notification.Description, notification.IsRead);
        }
    }
}