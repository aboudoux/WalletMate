using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class SpendingAdded : DomainEvent, IOperation
    {
        public SpendingAdded(int operationId, double amount, string label, PairInfo pair, SpendingOperationTypeInfo type)
        {
            Pair = pair;
            Label = label;
            Amount = amount;
            Type = type;
            OperationId = operationId;
        }

        public int OperationId { get; }
        public PairInfo Pair { get;  } 
        public string Label { get;  }
        public double Amount { get;  }
        public SpendingOperationTypeInfo Type { get;  } 
    }
}