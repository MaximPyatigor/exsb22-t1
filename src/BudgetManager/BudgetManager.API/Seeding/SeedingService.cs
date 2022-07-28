using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.CQRS.Commands.CurrencyCommands;
using BudgetManager.CQRS.Commands.DefaultCategoryCommands;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Commands.UserCommands;
using BudgetManager.CQRS.Queries.CategoryQueries;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Queries.DefaultCategoryQueries;
using BudgetManager.CQRS.Queries.NotificationQueries;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.Models.MongoDB.Models.Interfaces;
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
            var task1 = SeedCountries();
            var task2 = SeedCurrencies();
            var task3 = SeedCategories();
            var task4 = SeedUsers();

            // Wait for all of the seeding functions to finish before moving on. This way program doesn't start
            // before making sure, that every needed document is seeded and in place.
            Task.WaitAll(task1, task2, task3, task4);
        }

        public async Task SeedCountries()
        {
            var countriesList = await _mediator.Send(new GetCountryListQuery());

            // Check if null or empty
            if (countriesList == null || !countriesList.Any())
            {
                Console.WriteLine("No countries found in the database.");

                var countriesCurrenciesPath = ".\\Seeding\\Seeds\\country_currency.json";
                string jsonCountriesCurrencies = File.ReadAllText(countriesCurrenciesPath);

                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(jsonCountriesCurrencies);
                if (countries == null)
                {
                    Console.WriteLine("No default countries to seed found");
                    return;
                }

                Console.WriteLine("Seeding Countries...");
                await _mediator.Send(new AddManyCountriesCommand(countries));
                Console.WriteLine("Seeding Countries successful.");
            }
        }

        public async Task SeedCurrencies()
        {
            var currenciesList = await _mediator.Send(new GetCurrencyListQuery());

            // Check if null or empty
            if (currenciesList == null || !currenciesList.Any())
            {
                Console.WriteLine("No currencies found in the database.");

                var countriesCurrenciesPath = ".\\Seeding\\Seeds\\currencies.json";
                string jsonCountriesCurrencies = File.ReadAllText(countriesCurrenciesPath);

                var currencies = JsonConvert.DeserializeObject<IEnumerable<Currency>>(jsonCountriesCurrencies);
                if (currencies == null)
                {
                    Console.WriteLine("No default currencies to seed found");
                    return;
                }

                Console.WriteLine("Seeding Currencies...");
                await _mediator.Send(new AddManyCurrenciesCommand(currencies));
                Console.WriteLine("Seeding Currencies successful.");
            }
        }

        public async Task SeedCategories()
        {
            var categoriesList = await _mediator.Send(new GetDefaultCategoriesQuery());

            // Check if null or empty
            if (categoriesList == null || !categoriesList.Any())
            {
                Console.WriteLine("No categories found in the database.");

                var categoriesPath = ".\\Seeding\\Seeds\\default-categories.json";
                string jsonCategories = File.ReadAllText(categoriesPath);

                var categories = JsonConvert.DeserializeObject<IEnumerable<DefaultCategory>>(jsonCategories);
                if (categories == null)
                {
                    Console.WriteLine("No default categories to seed found");
                    return;
                }

                Console.WriteLine("Seeding Categories...");
                await _mediator.Send(new AddManyDefaultCategoriesCommand(categories));
                Console.WriteLine("Seeding Categories successful.");
            }
        }

        public async Task SeedUsers()
        {
            var usersList = await _mediator.Send(new GetUsersQuery());

            // Check if null or empty
            if (usersList == null || !usersList.Any())
            {
                Console.WriteLine("No users found in the database.");

                var usersPath = ".\\Seeding\\Seeds\\users.json";
                string usersCategories = File.ReadAllText(usersPath);

                var users = JsonConvert.DeserializeObject<IEnumerable<AddUserDTO>>(usersCategories);
                if (users == null)
                {
                    Console.WriteLine("No default users to seed found");
                    return;
                }

                Console.WriteLine("Seeding Users...");
                await _mediator.Send(new AddManyUsersCommand(users));
                Console.WriteLine("Seeding Users successful.");
            }
        }
    }
}
