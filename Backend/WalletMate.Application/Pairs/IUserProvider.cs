using System.Collections.Generic;

namespace WalletMate.Application.Pairs
{
    public interface IUserProvider
    {
        IReadOnlyList<IUser> GetUsers();

        IConfiguredPair GetConfiguredPair();
    }
}