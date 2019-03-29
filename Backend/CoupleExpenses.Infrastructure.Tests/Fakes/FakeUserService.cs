using CoupleExpenses.Domain.Common;

namespace CoupleExpenses.Infrastructure.Tests.Fakes
{
    public class FakeUserService : IUserService
    {
        public string GetCurrentUserName()
        {
            return "TEST USER";
        }
    }
}