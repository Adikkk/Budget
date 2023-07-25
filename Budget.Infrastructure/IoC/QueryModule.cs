using Autofac;
using Budget.Application.Query.Abstractions;
using Budget.Infrastructure.Dispatchers;
using Budget.Infrastructure.Persistence;
using System.Reflection;

namespace Budget.Infrastructure.IoC
{
    public class QueryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IQueryHandler<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<WriteDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .SingleInstance();
        }
    }
}
