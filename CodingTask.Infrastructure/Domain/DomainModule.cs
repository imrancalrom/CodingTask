using Autofac;
using CodingTask.Application.Customers.DomainServices;
using CodingTask.Domain.Customers;


namespace CodingTask.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

         
        }
    }
}