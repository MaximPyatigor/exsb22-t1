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
            services.AddSingleton<NotificationJob>();

            List<JobMetadata> jobMetadatas = new List<JobMetadata>();
            // https://crontab.guru/
            // Every 10 seconds
            // jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));

            // Every day at 06 : 00 UTC
            // jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0 6 * * * ?"));

            services.AddSingleton(jobMetadatas);
            services.AddHostedService<MyScheduler>();
        }
    }
}
