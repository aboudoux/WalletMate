using WalletMate.Domain.Common;

namespace WalletMate.Infrastructure.Tests.Fakes
{
    public class FakeUserService : IUserService
    {
        public string GetCurrentUserName()
        {
            return "TEST USER";
        }
    }
}