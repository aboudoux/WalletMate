using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class RecipeAdded : DomainEvent, IOperation 
    {
        public RecipeAdded(int operationId, double amount, string label, PairInfo pair, RecipeOperationTypeInfo type)
        {
            Pair = pair;
            Label = label;
            Amount = amount;
            Type = type;
            OperationId = operationId;
        }

        public int OperationId { get; }
        public PairInfo Pair { get; }
        public string Label { get; }
        public double Amount { get; }
        public RecipeOperationTypeInfo Type { get; }
    }
}