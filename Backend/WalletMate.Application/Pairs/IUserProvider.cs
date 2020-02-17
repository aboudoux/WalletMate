using System.Collections.Generic;
using WalletMate.Application.Pairs.Queries;

namespace WalletMate.Application.Pairs
{
    public interface IUserProvider
    {
        IReadOnlyList<IUser> GetUsers();

        IConfiguredPair GetConfiguredPair();
    }
}