using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WalletMate.Application;
using WalletMate.Application.Core;

namespace WalletMate.BlazorApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webHost = CreateHostBuilder(args).Build();

			using (var scope = webHost.Services.CreateScope()) {
				var commandBus = scope.ServiceProvider.GetRequiredService<ICommandBus>();
				await commandBus.SendAsync(new ReplayAllEvents());
			}

			await webHost.RunAsync();

			//CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
