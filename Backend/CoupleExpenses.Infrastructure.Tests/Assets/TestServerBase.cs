using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using CoupleExpenses.Domain.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoupleExpenses.Infrastructure.Tests.Assets
{
    public class TestServerBase<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _testServer;

        private readonly string _environment;
        private readonly string _apiAssemblyName;

        public HttpClient Client { get; }

        public IServiceProvider ServiceProvider { get; }

        public TestServerBase(string environment, string apiAssemblyName, Action<IServiceCollection> configureServices = default)
        {
            _environment = environment;
            _apiAssemblyName = apiAssemblyName;

            var contentRootPath = ResolveContentPath();

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRootPath)
                .UseEnvironment(_environment)
                .UseConfiguration(new ConfigurationBuilder().SetBasePath(contentRootPath)
                    .AddJsonFile($"appsettings.{_environment}.json").Build())
                .UseStartup<TStartup>();

            if (configureServices != default)
                builder.ConfigureServices(configureServices);

            _testServer = new TestServer(builder);

            ServiceProvider = _testServer.Host.Services;
            Client = _testServer.CreateClient();

        }

        private string ResolveContentPath()
        {
            return "C:\\temp\\CoupleExpenses\\CoupleExpenses.WebApp";
            var apiDirectory = _apiAssemblyName.Split('.').Last();
            var startDirectory = Path.GetDirectoryName(Assembly.Load(_apiAssemblyName).Location);
            while (JsonFileDoesntExists() && !startDirectory.IsEmpty())
            {
                startDirectory = Directory.GetParent(startDirectory).FullName;
            }

            if (startDirectory.IsEmpty())
            {
                throw new DirectoryNotFoundException("cannot find test server directory");
            }

            return Path.Combine(startDirectory, apiDirectory);

            bool JsonFileDoesntExists()
                => !File.Exists(Path.Combine(startDirectory, apiDirectory, $"appsettings.{_environment}.json"));
        }

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
