using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WalletMate.Application;
using WalletMate.Application.Core;

namespace WalletMate.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var commandBus = scope.ServiceProvider.GetRequiredService<ICommandBus>();
                await commandBus.SendAsync(new ReplayAllEvents());
            }

            await webHost.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();        
    }
}
