using AutoMapper;
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
        private readonly IBaseRepository<Notification> _dataAccess;
        private readonly IMapper _mapper;

        public UpdateNotificationReadStatusHandler(IBaseRepository<Notification> dataAccess, IMapper mapper)
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
