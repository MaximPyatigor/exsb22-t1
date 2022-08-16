using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Model.Enums;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class AddNotificationToAllHandler : IRequestHandler<AddNotificationToAllCommand, Guid>
    {
        private readonly IBaseRepository<User> _userContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddNotificationToAllHandler(IBaseRepository<User> userContext, IMapper mapper, IMediator mediator)
        {
            _userContext = userContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(AddNotificationToAllCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(request.NotificationDto);

            var userIds = (await _mediator.Send(new GetUsersQuery(), cancellationToken)).Select(u => u.Id);

            foreach (var userId in userIds)
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
                var update = Builders<User>.Update.Push(u => u.Notifications, notification);

                var updatedUser = await _userContext.UpdateOneAsync(filter, update, cancellationToken);
                if (updatedUser == null) { throw new KeyNotFoundException("UserId not found"); }
            }

            return (Guid)notification.Id;
        }
    }
}
