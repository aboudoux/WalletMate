using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WalletMate.Application.Core;
using WalletMate.Application.Periods;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Tests.Fakes;
using Xunit;

namespace WalletMate.Infrastructure.Tests
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