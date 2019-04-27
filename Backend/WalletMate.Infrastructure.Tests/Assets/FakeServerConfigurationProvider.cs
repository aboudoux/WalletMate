using System.Collections.Generic;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Services;

namespace WalletMate.Infrastructure.Tests.Assets
{
    public class FakeServerConfigurationProvider : IConfigurationProvider
    {
        public IReadOnlyList<User> GetUsers()
        {
            return new List<User>()
            {
                new User("Aurélien", "1234"),
                new User("Marie", "1234")
            };
        }

        public string GetFirstPairUserName() => "Aurélien";

        public string GetSecondPairUserName() => "Marie";
    }
}