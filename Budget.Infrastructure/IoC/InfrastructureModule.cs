using Autofac;
using Budget.Domain.Shared;
using Budget.Infrastructure.Bus;
using RabbitMQ.Client;

namespace Budget.Infrastructure.IoC
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<RabbitMQPersistentConnection>()
                .As<IPersistentConnection<IModel>>()
                .SingleInstance();

            builder
                .RegisterType<RabbitMQEventBus>()
                .As<IEventBus>()
                .SingleInstance();
        }
    }
}
