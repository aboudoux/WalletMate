using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("SpendingOperationTypeChanged")]
    public sealed class SpendingOperationTypeChanged : DomainEvent
    {
        public SpendingOperationTypeChanged(OperationId operationId, SpendingOperationType operationType)
        {
            OperationId = operationId;
            OperationType = operationType;
        }

        public OperationId OperationId { get; }
        public SpendingOperationType OperationType { get; }
    }
}