using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeOperationTypeChanged")]
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