using System;
using System.Threading.Tasks;

namespace CoupleExpenses.Infrastructure.Services
{
    public interface IAuthorizationService
    {
        Task<Guid> Authenticate(string username, string password);
        Task<string> GetAssociatedUser(Guid authKey);
        Task Logoff(Guid authKey);
    }
}