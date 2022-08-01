using AutoMapper;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class UpdateNotificationReadStatusHandler : IRequestHandler<UpdateNotificationReadStatusCommand, NotificationResponse>
    {
        private readonly INotificationRepository _dataAccess;
        private readonly IMapper _mapper;

        public UpdateNotificationReadStatusHandler(INotificationRepository dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> Handle(UpdateNotificationReadStatusCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Notification>.Filter.Eq(x => x.Id, request.Id);
            var update = Builders<Notification>.Update.Set(x => x.IsRead, request.IsRead);

            Notification notification = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);
            return _mapper.Map<NotificationResponse>(notification);
        }
    }
}
