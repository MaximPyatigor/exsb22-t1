using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.CQRS.Seeding
{
    public class SeedingService : ISeedingService
    {
        private readonly IMediator _mediator;

        public SeedingService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public void Seed()
        {
            var noti = new AddNotificationDto();
            noti.Description = "Seed test2";
            _mediator.Send(new AddNotificationCommand(noti));
        }
    }
}
