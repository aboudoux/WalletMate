using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("SpendingCategoryChanged")]
    public sealed class SpendingCategoryChanged : DomainEvent
    {
        public SpendingCategoryChanged(OperationId operationId, SpendingCategory category)
        {
            OperationId = operationId;
            Category = category;
        }

        public OperationId OperationId { get; }
        public SpendingCategory Category { get; }
    }
}