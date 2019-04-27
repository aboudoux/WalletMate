using WalletMate.Application.Core;

namespace WalletMate.Application.Pairs
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

