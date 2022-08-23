using AutoMapper;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuery, IEnumerable<NotificationResponse>>
    {
        private readonly IBaseRepository<User> _userContext;
        private readonly IMapper _mapper;

        public GetNotificationListHandler(IBaseRepository<User> userContext, IMapper mapper)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<NotificationResponse>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            // Retrieve User from the database only with Notification field, then map the list to response.

            var definition = Builders<User>.Projection
                .Exclude(x => x.Id)
                .Include(x => x.Notifications);

            var filter = Builders<User>.Filter.Eq(u => u.Id, request.UserId);

            var filteredUser = (await _userContext
                .FilterByAsync<User>(filter, definition, cancellationToken))
                .FirstOrDefault();

            if (filteredUser == null) { throw new KeyNotFoundException("UserId or notificationId not found"); }

            var result = _mapper.Map<IEnumerable<NotificationResponse>>(filteredUser.Notifications);
            return result;
        }
    }
}