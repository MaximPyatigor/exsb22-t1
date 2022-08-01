using AutoMapper;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuery, IEnumerable<NotificationResponse>>
    {
        private readonly INotificationRepository _dataAccess;
        private readonly IMapper _mapper;

        public GetNotificationListHandler(INotificationRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationResponse>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<NotificationResponse>>(await _dataAccess.GetListByUserIdAsync(request.userId, cancellationToken));
            return result;
        }
    }
}
