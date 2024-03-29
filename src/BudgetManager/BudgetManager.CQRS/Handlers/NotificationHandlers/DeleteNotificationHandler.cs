﻿using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly IBaseRepository<User> _userContext;

        public DeleteNotificationHandler(IBaseRepository<User> userContext)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.UserId);
            var notificationFilter = Builders<User>.Filter.ElemMatch(x => x.Notifications,
                Builders<Notification>.Filter.Eq(y => y.Id, request.Id));

            var filter = userFilter & notificationFilter;

            var update = Builders<User>.Update.PullFilter(u => u.Notifications, n => n.Id == request.Id );
            var updatedUser = await _userContext.UpdateOneAsync(filter, update, cancellationToken);

            if (updatedUser == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }
            return true;
        }
    }
}
