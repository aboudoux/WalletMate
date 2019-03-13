using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class RecipeOperationTypeChanged : DomainEvent 
    {
        public RecipeOperationTypeChanged(OperationId operationId, RecipeOperationType operationType) {
            OperationId = operationId.Value;
            OperationType = operationType.Value;
        }

        public int OperationId { get; }
        public int OperationType { get; }
    }
}