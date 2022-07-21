using BudgetManager.CQRS.Commands.NotificationCommands;
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
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public DeleteNotificationHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            await _dataAccess.DeleteByIdAsync(request.Id);
            //This returns nothing
            return Unit.Value;
        }
    }
}
