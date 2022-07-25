using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Handlers.NotificationHandlers
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuery, IEnumerable<Notification>>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public GetNotificationListHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Notification>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            return await _dataAccess.GetAllAsync(cancellationToken);
        }
    }
}
