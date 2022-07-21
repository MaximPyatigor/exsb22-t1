using BudgetManager.CQRS.Commands.NotificationCommands;
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
    public class UpdateNotificationReadStatusHandler : IRequestHandler<UpdateNotificationReadStatusCommand, NotificationResponse>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public UpdateNotificationReadStatusHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<NotificationResponse> Handle(UpdateNotificationReadStatusCommand request, CancellationToken cancellationToken)
        {
            var notification = await _dataAccess.FindByIdAsync(request.Id);
            if (notification == null) return null;

            notification.IsRead = request.IsRead;

            await _dataAccess.ReplaceOneAsync(notification);
            return new NotificationResponse(notification.Id, notification.NotificationType, notification.Description, notification.IsRead);
        }
    }
}
