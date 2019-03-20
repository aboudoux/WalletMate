using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("492915bb-e114-48d9-8551-844d834f21d8")]
    public sealed class SpendingAdded : DomainEvent, IOperation
    {
        public SpendingAdded(OperationId operationId, Amount amount, Label label, Pair pair, SpendingOperationType type)
        {
            Pair = pair;
            Label = label;
            Amount = amount;
            Type = type;
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
        public Pair Pair { get;  } 
        public Label Label { get;  }
        public Amount Amount { get;  }
        public SpendingOperationType Type { get;  } 
    }
}