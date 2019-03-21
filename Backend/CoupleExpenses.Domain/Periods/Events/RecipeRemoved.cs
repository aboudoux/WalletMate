using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeRemoved")]
    public sealed class RecipeRemoved : DomainEvent 
    {
        public RecipeRemoved(OperationId operationId) 
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}