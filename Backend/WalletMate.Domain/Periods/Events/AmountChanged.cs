using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("AmountChanged")]
    public sealed class AmountChanged : DomainEvent
    {
        public AmountChanged(OperationId operationId, Amount amount) 
        {
            OperationId = operationId;
            Amount = amount;
        }

        public OperationId OperationId { get; }

        public Amount Amount { get; }
    }
}