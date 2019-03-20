using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("22aa52ed-4ae3-4f39-9e30-ca3b9a921129")]
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