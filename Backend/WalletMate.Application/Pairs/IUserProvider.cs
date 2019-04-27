using System.Collections.Generic;

namespace WalletMate.Application.Pairs
{
    public interface IUserProvider
    {
        IReadOnlyList<IUser> GetUsers();

        string GetFirstPairUserName();
        string GetSecondPairUserName();
    }
}