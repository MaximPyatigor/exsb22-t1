using Quartz;
using Quartz.Spi;

namespace BudgetManager.Scheduler.JobFactory
{
    class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider service;
        public MyJobFactory(IServiceProvider serviceProvider)
        {
            service = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return (IJob)service.GetService(jobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}
