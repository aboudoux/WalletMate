using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("0d739d6c-8c07-4fd7-928d-db3b12a01e86")]
    public sealed class SpendingRemoved : DomainEvent
    {        
        public SpendingRemoved(OperationId operationId)
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}