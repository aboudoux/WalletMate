using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("PairChanged")]
    public sealed class PairChanged : DomainEvent
    {
        public PairChanged(OperationId operationId, Pair pair)
        {
            OperationId = operationId;
            Pair = pair;
        }
        public OperationId OperationId { get; }
        public Pair Pair { get; }
    }
}