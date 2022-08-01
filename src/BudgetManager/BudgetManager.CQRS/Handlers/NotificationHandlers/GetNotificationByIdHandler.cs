using AutoMapper;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, NotificationResponse>
    {
        private readonly INotificationRepository _dataAccess;
        private readonly IMapper _mapper;

        public GetNotificationByIdHandler(INotificationRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _dataAccess.FindByIdAsync(request.Id, cancellationToken);
            return notification == null ? null : _mapper.Map<NotificationResponse>(notification);
        }
    }
}