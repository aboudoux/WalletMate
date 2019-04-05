using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeCategoryChanged")]
    public sealed class RecipeCategoryChanged : DomainEvent 
    {
        public RecipeCategoryChanged(OperationId operationId, RecipeCategory category) {
            OperationId = operationId;
            Category = category;
        }

        public OperationId OperationId { get; }
        public RecipeCategory Category { get; }
    }
}