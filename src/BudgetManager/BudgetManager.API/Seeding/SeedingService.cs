using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using Newtonsoft.Json;

namespace BudgetManager.API.Seeding
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
            var path = ".\\Seeding\\Seeds\\default-categories.json";
            string jsonInhabitants = File.ReadAllText(path);

            var defaultCategories = JsonConvert.DeserializeObject<List<DefaultCategory>>(jsonInhabitants);
            if (defaultCategories == null)
            {
                Console.WriteLine("No default categories to seed found");
                return;
            }

            Console.WriteLine(defaultCategories[0].Name);

/*            var noti = new AddNotificationDto();
            noti.Description = "Seed test3";
            _mediator.Send(new AddNotificationCommand(noti));*/
        }
    }
}
