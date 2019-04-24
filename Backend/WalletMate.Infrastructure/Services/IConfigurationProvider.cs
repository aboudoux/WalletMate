using System.Collections.Generic;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.Services
{
    public interface IConfigurationProvider
    {
        IReadOnlyCollection<User> GetUsers();
    }
}