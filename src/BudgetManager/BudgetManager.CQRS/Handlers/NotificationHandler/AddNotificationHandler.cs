using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandler
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationCommand, NotificationResponse>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public AddNotificationHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<NotificationResponse> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return null; }
            Notification notification = new Notification()
            {
                NotificationType = (NotificationTypes)request.Type,
                Description = request.Description, 
                IsRead = request.IsRead,
            };

            await _dataAccess.InsertOneAsync(notification);
            return new NotificationResponse(notification.Id.ToString(), notification.NotificationType, notification.Description, notification.IsRead);
        }
    }
}
