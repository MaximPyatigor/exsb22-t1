using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly INotificationRepository _dataAccess;

        public DeleteNotificationHandler(INotificationRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _dataAccess.DeleteByIdAsync(request.Id, cancellationToken);
            return isDeleted;
        }
    }
}
