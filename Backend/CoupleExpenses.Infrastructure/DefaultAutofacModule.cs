using Autofac;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Infrastructure
{
    public class DefaultAutofacModule : Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MediatorCommandBus>().As<ICommandBus>().SingleInstance();
            builder.RegisterType<MediatorQueryBus>().As<IQueryBus>().SingleInstance();
            builder.RegisterType<InMemoryDatabaseRepository>().As<IDatabaseRepository>().SingleInstance();
            
            builder.RegisterType<EventBroker>().As<IEventBroker>().SingleInstance();
            builder.RegisterType<FileEventStoreWithCache>().As<IEventStore>().SingleInstance();
            builder.RegisterType<MediatorEventDispatcher>().As<IEventDispatcher>().SingleInstance();
            builder.RegisterType<CustomJsonSerializer>().As<ISerializer>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
        }
    }
}