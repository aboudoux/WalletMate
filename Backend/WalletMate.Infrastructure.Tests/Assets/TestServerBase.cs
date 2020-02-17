using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WalletMate.Infrastructure.WebAppTests.Assets
{
    public class TestServerBase<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _testServer;

        public HttpClient Client { get; }

        public IServiceProvider ServiceProvider { get; }

        public TestServerBase(string environment, string apiAssemblyName, Action<IServiceCollection> configureServices = default)
        {
            var contentRootPath = ResolveContentPath();

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRootPath)
                .UseEnvironment(environment)
                .UseConfiguration(new ConfigurationBuilder().SetBasePath(contentRootPath)
                    .AddJsonFile($"appsettings.{environment}.json").Build())
                .UseStartup<TStartup>();

            if (configureServices != default)
                builder.ConfigureServices(configureServices);

            _testServer = new TestServer(builder);

            ServiceProvider = _testServer.Host.Services;
            Client = _testServer.CreateClient();
        }

        private static string ResolveContentPath() => Path.GetDirectoryName(Assembly.GetAssembly(typeof(TStartup)).Location);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _testServer?.Dispose();
                Client?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
