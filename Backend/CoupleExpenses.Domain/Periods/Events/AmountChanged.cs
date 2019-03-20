using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("c95047a7-5573-4a34-85da-363bac8db01e")]
    public sealed class AmountChanged : DomainEvent
    {
        public AmountChanged(OperationId operationId, Amount amount) 
        {
            OperationId = operationId.Value;
            Amount = amount.Value;
        }

        public int OperationId { get; }

        public double Amount { get; }
    }
}