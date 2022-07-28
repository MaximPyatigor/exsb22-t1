using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationCommand, Guid>
    {
        private readonly IBaseRepository<Notification> _dataAccess;
        private readonly IMapper _mapper;

        public AddNotificationHandler(IBaseRepository<Notification> dataAccess, IMapper mapper)
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
