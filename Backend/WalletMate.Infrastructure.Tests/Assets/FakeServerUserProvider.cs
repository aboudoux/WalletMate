using System.Collections.Generic;
using WalletMate.Application.Pairs;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.WebAppTests.Assets
{
    public class FakeServerUserProvider : IUserProvider
    {
        public IReadOnlyList<IUser> GetUsers()
        {
            return new List<User>
            {
                new User("Aurélien", "1234"),
                new User("Marie", "1234")
            };
        }

        public IConfiguredPair GetConfiguredPair() => new ConfiguredPair("Aurélien", "Marie");
    }
}