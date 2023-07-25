using Autofac;
using Budget.Domain.Expenses;
using Budget.Domain.Shared;
using Budget.Infrastructure.Dispatchers;
using Budget.Infrastructure.Persistence;
using System.Reflection;

namespace Budget.Infrastructure.IoC
{
    public class EventModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IEventHandler<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerLifetimeScope();

            builder
               .RegisterType<WriteDbContext>()
               .AsSelf()
               .InstancePerLifetimeScope();

            builder
                .RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .SingleInstance();
        }
    }
}
