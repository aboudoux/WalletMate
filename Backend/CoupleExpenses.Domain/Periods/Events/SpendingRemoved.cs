using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class SpendingRemoved : DomainEvent
    {        
        public SpendingRemoved(int operationId)
        {
            OperationId = operationId;
        }

        public int OperationId { get; }
    }
}