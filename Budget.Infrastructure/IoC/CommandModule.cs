using Autofac;
using Budget.Application.Command.Abstractions;
using Budget.Domain.Expenses;
using Budget.Domain.Incomes;
using Budget.Infrastructure.Dispatchers;
using Budget.Infrastructure.Persistence.Repository;
using System.Reflection;

namespace Budget.Infrastructure.IoC
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterType<ExpenseWriteOnlyRepository>()
               .As<IExpenseWriteOnlyRepository>()
               .InstancePerLifetimeScope();

            builder
               .RegisterType<IncomeWriteOnlyRepository>()
               .As<IIncomeWriteOnlyRepository>()
               .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(ICommandHandler<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(ICommandHandler<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .SingleInstance();
        }
    }
}
