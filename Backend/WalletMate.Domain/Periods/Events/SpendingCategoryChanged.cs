using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
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