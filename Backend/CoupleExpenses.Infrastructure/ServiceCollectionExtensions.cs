using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoupleExpenses.Application.Core;
using Mediator.Net;
using Mediator.Net.Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace CoupleExpenses.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider GetAutofacProvider(this IServiceCollection services, Action<ContainerBuilder> extraConfiguration = null)
        {
            var mediaBuilder = new MediatorBuilder();            
            mediaBuilder.RegisterHandlers(typeof(ICommandBus).Assembly, typeof(ISerializer).Assembly);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);            
            containerBuilder.RegisterMediator(mediaBuilder);
            containerBuilder.RegisterModule<DefaultAutofacModule>();

            extraConfiguration?.Invoke(containerBuilder);

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }      
}