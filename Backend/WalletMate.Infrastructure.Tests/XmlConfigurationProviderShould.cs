using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Services;
using WalletMate.Infrastructure.Services.Exceptions;
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

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "1234", "1234")]
        [InlineData("", "", "1234")]
        [InlineData("1234", "", "1234")]
        [InlineData("1234", "1234", "")]
        public async Task Throw_exception_if_blank_password(string firstPassword, string secondPassword, string operatorPassword)
        {
            await Make.TestFile("blankpassword.config").AndExecute(environment =>
            {
                CreateConfigFile(environment.FilePath, firstUserPassword: firstPassword, secondUserPassword: secondPassword, operatorPassword: operatorPassword);

                Action action = () => new XmlConfigurationProvider(environment.FilePath);
                action.Should().Throw<EmptyConfigurationPasswordException>();

                return Task.CompletedTask;
            });
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "Testuser", "Testuser")]
        [InlineData("", "", "Testuser")]
        [InlineData("Testuser", "", "Testuser")]
        [InlineData("Testuser", "Testuser", "")]
        public async Task Throw_exception_if_blank_username(string firstUsername, string secondUsername, string operatorUsername)
        {
            await Make.TestFile("blankname.config").AndExecute(environment =>
            {
                CreateConfigFile(environment.FilePath, firstUsername: firstUsername, secondUsername: secondUsername, operatorUsername: operatorUsername);

                Action action = () => new XmlConfigurationProvider(environment.FilePath);
                action.Should().Throw<EmptyConfigurationUsernameException>();

                return Task.CompletedTask;
            });
        }

        [Theory]
        [InlineData("1234", "7110eda4d09e062aa5e4a390b0a572ac0d2c0220")]
        [InlineData("Password", "8be3c943b1609fffbfc51aad666d0a04adf83c9d")]
        [InlineData("Azerty", "0f300f33b728cabd2cd5cbde86757722de291ceb")]
        public async Task Return_users_with_decoded_password_hash(string allPassword, string expected)
        {
            await Make.TestFile("blankname.config").AndExecute(environment =>
            {
                CreateConfigFile(environment.FilePath, firstUserPassword: allPassword, secondUserPassword: allPassword, operatorPassword: allPassword);
                var provider = new XmlConfigurationProvider(environment.FilePath);

                provider.GetUsers()
                    .Select(a => a.Password)
                    .ToList()
                    .Should()
                    .HaveCount(3)
                    .And
                    .BeEquivalentTo(
                        new List<string>() { expected, expected, expected });

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