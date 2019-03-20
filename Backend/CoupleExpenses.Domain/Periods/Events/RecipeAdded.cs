using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("64f7b8db-0f8d-45d4-a29e-2a1278bdef33")]
    public sealed class RecipeAdded : DomainEvent, IOperation 
    {
        public RecipeAdded(OperationId operationId, Amount amount, Label label, Pair pair, RecipeOperationType type)
        {
            Pair = pair;
            Label = label;
            Amount = amount;
            Type = type;
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
        public Pair Pair { get; }
        public Label Label { get; }
        public Amount Amount { get; }
        public RecipeOperationType Type { get; }
    }
}