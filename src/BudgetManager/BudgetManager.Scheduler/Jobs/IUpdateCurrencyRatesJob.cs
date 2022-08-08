using Quartz;

namespace BudgetManager.Scheduler.Jobs
{
    public interface IUpdateCurrencyRatesJob
    {
        Task Execute(IJobExecutionContext context);
        Task UpdateCurrencyRates();
    }
}