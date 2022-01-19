using Autofac;
using CodingTask.Application.Customers.DomainServices;
using CodingTask.Domain.Customers;
using CodingTask.Domain.ForeignExchange;
using CodingTask.Infrastructure.Domain.ForeignExchanges;

namespace CodingTask.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ForeignExchange>()
                .As<IForeignExchange>()
                .InstancePerLifetimeScope();
        }
    }
}