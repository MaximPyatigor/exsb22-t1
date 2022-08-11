using BudgetManager.Scheduler.JobFactory;
using BudgetManager.Scheduler.Jobs;
using BudgetManager.Scheduler.Models;
using BudgetManager.Scheduler.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;

namespace BudgetManager.Scheduler
{
    public class SchedulerService
    {
        public static async void AddQuartz(IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, MyJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<UpdateCurrencyRatesJob>();
            List<JobMetadata> jobMetadatas = new List<JobMetadata>();

            // Update currencies every day at 12:00PM UTC
            jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(UpdateCurrencyRatesJob), "Update Currency Rates Job", "00 12 * * * ?"));
            services.AddSingleton(jobMetadatas);
            services.AddHostedService<MyScheduler>();
        }
    }
}
