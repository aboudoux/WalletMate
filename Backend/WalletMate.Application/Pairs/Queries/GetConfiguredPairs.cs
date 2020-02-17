using WalletMate.Application.Core;

namespace WalletMate.Application.Pairs.Queries
{
    public class GetConfiguredPair : IQuery<IConfiguredPair>
    {
        
    }

    public interface IConfiguredPair
    {
        string FirstPairName { get; }
        string SecondPairName { get; }
    }
}

