using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("73f07d3c-5951-43af-8c94-b9b5f595cea3")]
    public sealed class SpendingOperationTypeChanged : DomainEvent
    {
        public SpendingOperationTypeChanged(OperationId operationId, SpendingOperationType operationType)
        {
            OperationId = operationId.Value;
            OperationType = operationType.Value;
        }

        public int OperationId { get; }
        public int OperationType { get; }
    }
}