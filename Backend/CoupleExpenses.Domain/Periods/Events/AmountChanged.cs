using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
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