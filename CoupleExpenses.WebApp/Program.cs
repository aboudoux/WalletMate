using System.Threading.Tasks;
using CoupleExpenses.Application;
using CoupleExpenses.Application.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoupleExpenses.WebApp
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
