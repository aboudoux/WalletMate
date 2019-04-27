using System;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Services;

namespace WalletMate.Infrastructure
{
    public static class PairExtensions
    {
        public static string GetUserName(this Pair pair, IConfigurationProvider configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            return pair == Pair.First
                ? configuration.GetFirstPairUserName()
                : configuration.GetSecondPairUserName();
        }
    }
}