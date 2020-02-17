using WalletMate.Domain.Common;

namespace WalletMate.Infrastructure.WebAppTests.Fakes
{
    public class FakeConnectedUserService : IConnectedUserService
    {
        public string GetCurrentUserName()
        {
            return "TEST USER";
        }
    }
}