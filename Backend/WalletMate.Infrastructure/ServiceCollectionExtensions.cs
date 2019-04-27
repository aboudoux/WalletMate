using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WalletMate.Application.Core;
using WalletMate.Application.Pairs;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Infrastructure.Services;

namespace WalletMate.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, Action<IServiceCollection> config = null)
        {
            config?.Invoke(services);

            services.AddMediatR(Assembly.GetAssembly(typeof(ICommand)), Assembly.GetAssembly(typeof(ISerializer)));

            services.TryAddSingleton<IUserProvider, XmlUserProvider>();
            services.TryAddSingleton<IAuthorizationService, AuthorizationService>();
            services.TryAddSingleton<IEventStore, FileEventStoreWithCache>();
            services.TryAddSingleton<IDatabaseRepository, InMemoryDatabaseRepository>();
            services.TryAddSingleton<ISerializer, CustomJsonSerializer>();

            services.TryAddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddScoped<ICommandBus, MediatrCommandBus>();
            services.TryAddScoped<IQueryBus, MediatrQueryBus>();
            services.TryAddScoped<IEventBroker, EventBroker>();
            services.TryAddScoped<IEventDispatcher, MediatrEventDispatcher>();
            services.TryAddScoped<IConnectedUserService, ConnectedUserService>();
        }
    }
}