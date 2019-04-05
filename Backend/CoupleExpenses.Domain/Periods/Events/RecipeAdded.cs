using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("RecipeAdded")]
    public sealed class RecipeAdded : DomainEvent, IOperation 
    {
        public RecipeAdded(OperationId operationId, Amount amount, Label label, Pair pair, RecipeCategory category)
        {
            Pair = pair;
            Label = label;
            Amount = amount;
            Category = category;
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
        public Pair Pair { get; }
        public Label Label { get; }
        public Amount Amount { get; }
        public RecipeCategory Category { get; }
    }
}