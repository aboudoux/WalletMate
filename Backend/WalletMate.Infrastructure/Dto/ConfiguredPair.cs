using WalletMate.Application.Pairs;
using WalletMate.Application.Pairs.Queries;

namespace WalletMate.Infrastructure.Dto
{
    public class ConfiguredPair : IConfiguredPair
    {
        public ConfiguredPair(string firstPairName, string secondPairName)
        {
            FirstPairName = firstPairName;
            SecondPairName = secondPairName;
        }

        public string FirstPairName { get; }
        public string SecondPairName { get; }
    }
}