using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("SpendingRemoved")]
    public sealed class SpendingRemoved : DomainEvent
    {        
        public SpendingRemoved(OperationId operationId)
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}