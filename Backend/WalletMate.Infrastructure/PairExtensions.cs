using System;
using WalletMate.Application.Pairs;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Infrastructure
{
    public static class PairExtensions
    {
        public static string GetUserName(this Pair pair, IUserProvider user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return pair == Pair.First
                ? user.GetFirstPairUserName()
                : user.GetSecondPairUserName();
        }
    }
}