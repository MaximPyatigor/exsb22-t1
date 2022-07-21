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
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly IBaseRepository<Notification> _dataAccess;

        public DeleteNotificationHandler(IBaseRepository<Notification> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _dataAccess.FindByIdAsync(request.Id);
            if (notification == null) return false;

            await _dataAccess.DeleteByIdAsync(request.Id);
            return true;
        }
    }
}
