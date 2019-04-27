using WalletMate.Domain.Common;

namespace WalletMate.Infrastructure.Tests.Fakes
{
    public class FakeConnectedUserService : IConnectedUserService
    {
        public string GetCurrentUserName()
        {
            return "TEST USER";
        }
    }
}