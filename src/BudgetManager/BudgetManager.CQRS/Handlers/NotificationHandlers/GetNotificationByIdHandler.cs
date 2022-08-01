using AutoMapper;
using BudgetManager.CQRS.Projections.UserProjections;
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
            var definition = Builders<User>.Projection
                .Exclude(x => x.Id)
                .Include(x => x.Notifications);

            var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.UserId);

            var notificationFilter = Builders<User>.Filter.ElemMatch(x => x.Notifications,
                Builders<Notification>.Filter.Eq(y => y.Id, request.Id));

            var filter = userFilter & notificationFilter;

            var filteredUser = (await _userContext
                .FilterBy<UserNotificationListProjection>(filter, definition, cancellationToken))
                .FirstOrDefault();

            if (filteredUser == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }

            var result = _mapper.Map<NotificationResponse>(filteredUser.Notifications.FirstOrDefault());
            return result;
        }
    }
}