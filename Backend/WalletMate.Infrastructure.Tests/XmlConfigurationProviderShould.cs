using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using WalletMate.Infrastructure.Dto;
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
                Action action = () => new XmlConfigurationProvider(environment.FilePath);

                action.Should().Throw<FileNotFoundException>();
                File.Exists(environment.FilePath).Should().BeTrue();
                new FileInfo(environment.FilePath).Length.Should().BePositive();

                return Task.CompletedTask;
            });
        }

        [Theory]
        [InlineData("1234", "secret:3NcbCywQ+zhc+c43b/s3GkEx7kAEVMQfbqAvW3oUcfE3m3w3AGq+CLRuL8Hie2Oy")]
        [InlineData("password", "secret:UvIikN+fDwhL4DVCZVeJ+EtH986jtXuzN3x5E76P5EHlii+X3zY3oArvnF1dBdrM")]
        public async Task Crypt_password_if_stored_in_plain_text(string allPassword, string expected)
        {
            await Make.TestFile("password.config").AndExecute(environment =>
            {
                CreateConfigFile(environment.FilePath, firstUserPassword: allPassword, secondUserPassword:allPassword, operatorPassword:allPassword);

                var _ = new XmlConfigurationProvider(environment.FilePath);

                var config = Xml.DeserializeFrom<WalletMateConfiguration>(environment.FilePath);
                config.FirstPair.Password.Should().Be(expected);
                config.SecondPair.Password.Should().Be(expected);
                config.Operator.Password.Should().Be(expected);

                return Task.CompletedTask;
            });
        }

        private static void CreateConfigFile(string fileName, string firstUsername = "FirstUserName", string firstUserPassword = "password", string secondUsername = "SecondUsername", string secondUserPassword = "password", string operatorUsername = "Operator", string operatorPassword = "password")
        {
            var config = new WalletMateConfiguration();
            config.FirstPair = new User(firstUsername, firstUserPassword);
            config.SecondPair = new User(secondUsername, secondUserPassword);
            config.Operator = new User(operatorUsername, operatorPassword);
            Xml.SaveTo(fileName, config);
        }
    }
}