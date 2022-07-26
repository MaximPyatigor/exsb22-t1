using AutoMapper;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuery, IEnumerable<NotificationResponse>>
    {
        private readonly IBaseRepository<Notification> _dataAccess;
        private readonly IMapper _mapper;

        public GetNotificationListHandler(IBaseRepository<Notification> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationResponse>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<NotificationResponse>>(await _dataAccess.GetAllAsync(cancellationToken));
            return result;
        }
    }
}
