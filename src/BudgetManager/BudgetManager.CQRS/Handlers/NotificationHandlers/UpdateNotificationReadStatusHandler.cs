﻿using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class UpdateNotificationReadStatusHandler : IRequestHandler<UpdateNotificationReadStatusCommand, NotificationResponse>
    {
        private readonly IBaseRepository<User> _userContext;
        private readonly IMapper _mapper;

        public UpdateNotificationReadStatusHandler(IBaseRepository<User> userContext, IMapper mapper)
        {
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> Handle(UpdateNotificationReadStatusCommand request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.UserId);
            var notificationFilter = Builders<User>.Filter.ElemMatch(x => x.Notifications,
                Builders<Notification>.Filter.Eq(y => y.Id, request.Id));

            var filter = userFilter & notificationFilter;

            var update = Builders<User>.Update.Set(u => u.Notifications[-1].IsRead, request.IsRead);

            var updatedUser = await _userContext.UpdateOneAsync(filter, update, cancellationToken);

            if (updatedUser == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }

            var result = _mapper.Map<NotificationResponse>(updatedUser.Notifications.FirstOrDefault());
            return result;
        }
    }
}
