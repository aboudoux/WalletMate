using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("e365b925-5459-4391-86b2-2752c1a0f462")]
    public sealed class RecipeOperationTypeChanged : DomainEvent 
    {
        public RecipeOperationTypeChanged(OperationId operationId, RecipeOperationType operationType) {
            OperationId = operationId;
            OperationType = operationType;
        }

        public OperationId OperationId { get; }
        public RecipeOperationType OperationType { get; }
    }
}