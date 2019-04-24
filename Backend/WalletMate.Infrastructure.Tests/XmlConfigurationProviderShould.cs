using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using WalletMate.Infrastructure.Services;
using WalletMate.Infrastructure.Tests.Tools;
using Xunit;

namespace WalletMate.Infrastructure.Tests
{
    public class XmlConfigurationProviderShould
    {
        [Fact]
        public async Task Create_empty_config_file_and_throw_exception_when_config_not_exists()
        {
            await Make.TestFile("TestUser.config").AndExecute(environment =>
            {
                var file = "TestUser.config";
                Action action = () => new XmlConfigurationProvider(file);

                action.Should().Throw<FileNotFoundException>();
                File.Exists(file).Should().BeTrue();
                new FileInfo(file).Length.Should().BePositive();

                return Task.CompletedTask;
            });
        }
    }
}