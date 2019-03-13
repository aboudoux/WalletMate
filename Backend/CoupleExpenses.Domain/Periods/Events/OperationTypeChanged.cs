using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
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