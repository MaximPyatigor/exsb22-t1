using BudgetManager.CQRS.Commands.CountryCommands;
using BudgetManager.CQRS.Commands.CurrencyCommands;
using BudgetManager.CQRS.Commands.NotificationCommands;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.CQRS.Queries.CurrencyQueries;
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

        // This function is not async because we want to make sure that data is in place and seeded
        // before program launches.
        public void Seed()
        {
            SeedCountries();
            SeedCurrencies();
            SeedCategories();
        }

        public void SeedCountries()
        {
            var countriesList = _mediator.Send(new GetCountryListQuery()).GetAwaiter().GetResult();

            // Check if null or empty
            if (countriesList == null || !countriesList.Any())
            {
                Console.WriteLine("No countries found in the database.");

                var countriesCurrenciesPath = ".\\Seeding\\Seeds\\country-by-currency-code.json";
                string jsonCountriesCurrencies = File.ReadAllText(countriesCurrenciesPath);

                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(jsonCountriesCurrencies);
                if (countries == null)
                {
                    Console.WriteLine("No default countries to seed found");
                    return;
                }

                Console.WriteLine("Seeding Countries...");
                _mediator.Send(new AddManyCountriesCommand(countries)).GetAwaiter().GetResult();
                Console.WriteLine("Seeding Countries successful.");
            }
        }

        public void SeedCurrencies()
        {
            var currenciesList = _mediator.Send(new GetCurrencyListQuery()).GetAwaiter().GetResult();

            // Check if null or empty
            if (currenciesList == null || !currenciesList.Any())
            {
                Console.WriteLine("No currencies found in the database.");

                var countriesCurrenciesPath = ".\\Seeding\\Seeds\\country-by-currency-code.json";
                string jsonCountriesCurrencies = File.ReadAllText(countriesCurrenciesPath);

                var currencies = JsonConvert.DeserializeObject<IEnumerable<Currency>>(jsonCountriesCurrencies);
                if (currencies == null)
                {
                    Console.WriteLine("No default currencies to seed found");
                    return;
                }

                currencies = currencies.Distinct();

                Console.WriteLine("Seeding Currencies...");
                _mediator.Send(new AddManyCurrenciesCommand(currencies)).GetAwaiter().GetResult();
                Console.WriteLine("Seeding Currencies successful.");
            }
        }

        public void SeedCategories()
        {
 /*           var currenciesList = _mediator.Send(new GetCurrencyListQuery()).GetAwaiter().GetResult();

            // Check if null or empty
            if (currenciesList == null || !currenciesList.Any())
            {
                Console.WriteLine("No categories found in the database.");

                var categoriesPath = ".\\Seeding\\Seeds\\default-categories.json";
                string jsonCategories= File.ReadAllText(categoriesPath);

                var categories = JsonConvert.DeserializeObject<IEnumerable<Currency>>(jsonCategories);
                if (categories == null)
                {
                    Console.WriteLine("No default categories to seed found");
                    return;
                }

                Console.WriteLine("Seeding Categories...");
                _mediator.Send(new AddManyCategoriesCommand(categories)).GetAwaiter().GetResult();
                Console.WriteLine("Seeding Categories successful.");
            }*/
        }
    }
}
