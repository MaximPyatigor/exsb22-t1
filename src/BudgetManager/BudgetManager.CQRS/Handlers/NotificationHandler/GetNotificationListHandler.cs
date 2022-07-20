using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.NotificationHandler
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuery, IQueryable<Notification>>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public GetNotificationListHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IQueryable<Notification>> Handle(GetNotificationListQuery request,CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.AsQueryable());
        }
    }
}
