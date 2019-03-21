using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
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