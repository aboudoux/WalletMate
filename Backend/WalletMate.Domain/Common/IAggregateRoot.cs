using WalletMate.Domain.Common.Events;

namespace WalletMate.Domain.Common
{
    public interface IAggregateRoot
    {
        UncommittedEvents UncommittedEvents { get; }
    }
}