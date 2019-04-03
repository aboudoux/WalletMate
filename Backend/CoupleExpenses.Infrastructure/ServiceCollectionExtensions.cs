using System;
using System.Reflection;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoupleExpenses.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, Action<IServiceCollection> config = null)
        {
            config?.Invoke(services);

            services.TryAddSingleton<IAuthorizationService, AuthorizationService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddSingleton<IEventStore, FileEventStoreWithCache>();
            services.TryAddSingleton<ICommandBus, MediatrCommandBus>();
            services.TryAddSingleton<IQueryBus, MediatrQueryBus>();
            services.TryAddSingleton<IDatabaseRepository, InMemoryDatabaseRepository>();
            services.TryAddSingleton<IEventBroker, EventBroker>();
            services.TryAddSingleton<IEventDispatcher, MediatrEventDispatcher>();
            services.TryAddSingleton<ISerializer, CustomJsonSerializer>();
            services.TryAddSingleton<IUserService, UserService>();

            services.AddMediatR(Assembly.GetAssembly(typeof(ICommand)));
            services.AddMediatR(Assembly.GetAssembly(typeof(ISerializer)));
        }
    }
}