using BudgetManager.CQRS.Commands.CurrencyRatesCommands;
using BudgetManager.CQRS.Queries.CountryQueries;
using BudgetManager.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BudgetManager.Scheduler.Jobs
{
    public class UpdateCurrencyRatesJob : IJob, IUpdateCurrencyRatesJob
    {
        private readonly ILogger<UpdateCurrencyRatesJob> _logger;
        private readonly IMediator _mediator;

        public UpdateCurrencyRatesJob(ILogger<UpdateCurrencyRatesJob> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            UpdateCurrencyRates();
        }

        public async Task UpdateCurrencyRates()
        {
            _logger.LogInformation($"Update currency rates job: Job start at {DateTimeOffset.Now}");

            string jsonCurrencyRates;
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies/eur.json");
                var response = await client.GetAsync(endpoint);
                jsonCurrencyRates = await response.Content.ReadAsStringAsync();
            }

            var currencyRates = JsonConvert.DeserializeObject<CurrencyRates>(jsonCurrencyRates);
            if (currencyRates == null)
            {
                _logger.LogError($"Update currency rates job: FAILED Currency update at {DateTimeOffset.Now}. No currency rates received.");
                return;
            }

            _logger.LogInformation($"Update currency rates job: Seeding Currency Rates at {DateTimeOffset.Now}...");
            await _mediator.Send(new UpdateCurrencyRatesCommand(currencyRates), CancellationToken.None);
            _logger.LogInformation($"Update currency rates job: Seeding Currency Rates successful at {DateTimeOffset.Now}");
        }
    }
}
