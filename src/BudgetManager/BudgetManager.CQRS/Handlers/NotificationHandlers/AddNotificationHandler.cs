using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationCommand, Guid>
    {
        private readonly IBaseRepository<User> _userContext;
        private readonly IMapper _mapper;

        public AddNotificationHandler(IBaseRepository<User> userContext, IMapper mapper)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(request.NotificationDto);

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.UserId);
            var update = Builders<User>.Update.Push(u => u.Notifications, notification);

            var updatedUser = await _userContext.UpdateOneAsync(filter, update, cancellationToken);

            if (updatedUser == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }
            return (Guid)notification.Id;
        }
    }
}
