using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Application.Periods;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Tests.Fakes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoupleExpenses.Infrastructure.Tests
{
    public class PeriodCommandHanlderShould
    {
        [Fact]
        public async Task Run_CreatePeriod_Command()
        {
            IServiceCollection services = new ServiceCollection();

            services.RegisterDependencies(config =>
            {
                config.AddSingleton<IUserService, FakeUserService>();
                config.AddSingleton<IEventStore, FakeEventStore>();
            });

            var provider = services.BuildServiceProvider();
            var commandBus = provider.GetService<ICommandBus>();
            await commandBus.SendAsync(new CreatePeriod(PeriodName.From(1, 2001))); 
        }
    }
}