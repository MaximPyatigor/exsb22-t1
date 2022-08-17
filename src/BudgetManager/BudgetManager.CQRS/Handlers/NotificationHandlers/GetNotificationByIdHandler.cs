using AutoMapper;
using BudgetManager.CQRS.Queries.NotificationQueries;
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
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, NotificationResponse>
    {
        private readonly IBaseRepository<User> _userContext;
        private readonly IMapper _mapper;

        public GetNotificationByIdHandler(IBaseRepository<User> userContext, IMapper mapper)
        {
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var notificationId = request.Id;

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Notifications, c => c.Id == notificationId);
            var projection = Builders<User>.Projection.Include(u => u.Notifications)
                .ElemMatch(u => u.Notifications, c => c.Id == notificationId);

            var response = await _userContext.FilterBy<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }

            var usersNotification = user.Notifications.FirstOrDefault();
            var result = _mapper.Map<NotificationResponse>(usersNotification);
            return result;
        }
    }
}