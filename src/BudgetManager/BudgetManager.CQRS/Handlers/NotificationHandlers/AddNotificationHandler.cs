using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationCommand, string>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public AddNotificationHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<string> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return null; }
            Notification notification = new Notification()
            {
                NotificationType = (NotificationTypes)request.notificationDto.NotificationType,
                Description = request.notificationDto.Description,
            };

            await _dataAccess.InsertOneAsync(notification, cancellationToken);
            return notification.Id.ToString();
        }
    }
}
