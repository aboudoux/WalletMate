using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("603fdd6f-1abb-43bc-bcf2-0738d2ed8e2c")]
    public sealed class RecipeRemoved : DomainEvent 
    {
        public RecipeRemoved(OperationId operationId) 
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}