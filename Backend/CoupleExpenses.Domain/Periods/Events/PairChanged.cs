using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class PairChanged : DomainEvent
    {
        public PairChanged(OperationId operationId, Pair pair)
        {
            OperationId = operationId.Value;
            Pair = pair.Value;
        }
        public int OperationId { get; }
        public int Pair { get; }
    }
}