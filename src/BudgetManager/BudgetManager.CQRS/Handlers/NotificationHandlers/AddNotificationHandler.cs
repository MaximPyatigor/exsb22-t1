using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationCommand, Guid>
    {
        private readonly INotificationRepository _dataAccess;
        private readonly IMapper _mapper;

        public AddNotificationHandler(INotificationRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(request.NotificationDto);

            await _dataAccess.InsertOneAsync(notification, cancellationToken);
            return notification.Id;
        }
    }
}
