using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("SpendingRemoved")]
    public sealed class SpendingRemoved : DomainEvent
    {        
        public SpendingRemoved(OperationId operationId)
        {
            OperationId = operationId;
        }

        public OperationId OperationId { get; }
    }
}