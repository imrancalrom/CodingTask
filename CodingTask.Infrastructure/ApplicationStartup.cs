using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using CodingTask.Application.Configuration;

using CodingTask.Infrastructure.Caching;
using CodingTask.Infrastructure.Database;
using CodingTask.Infrastructure.Domain;

using CodingTask.Infrastructure.Processing;
using CodingTask.Infrastructure.Processing.InternalCommands;
using CodingTask.Infrastructure.Processing.Outbox;
using CodingTask.Infrastructure.Quartz;
using CodingTask.Infrastructure.SeedWork;
using Serilog;

namespace CodingTask.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            ICacheStore cacheStore,
           
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            bool runQuartz = true)
        {
          
            services.AddSingleton(cacheStore);

            var serviceProvider = CreateAutofacServiceProvider(
                services, 
                connectionString, 
                     executionContextAccessor);

            return serviceProvider;
        }

        private static IServiceProvider CreateAutofacServiceProvider(
            IServiceCollection services,
            string connectionString,
           
           
            IExecutionContextAccessor executionContextAccessor)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

           
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DomainModule());
            
        
            
            container.RegisterModule(new ProcessingModule());

            container.RegisterInstance(executionContextAccessor);

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            var serviceProvider = new AutofacServiceProvider(buildContainer);

            CompositionRoot.SetContainer(buildContainer);

            return serviceProvider;
        }

        private static void StartQuartz(
            string connectionString, 
          
            IExecutionContextAccessor executionContextAccessor)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();

           
            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DataAccessModule(connectionString));
          
            container.RegisterModule(new ProcessingModule());

            container.RegisterInstance(executionContextAccessor);
            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersContext>();
                dbContextOptionsBuilder.UseSqlServer(connectionString);

                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new OrdersContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            scheduler.JobFactory = new JobFactory(container.Build());

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }
    }
}