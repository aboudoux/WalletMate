using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
{
    [SerializableTypeIdentifier("LabelChanged")]
    public sealed class LabelChanged : DomainEvent
    {
        public LabelChanged(OperationId operationId, Label label)
        {
            OperationId = operationId;
            Label = label;
        }

        public OperationId OperationId { get; }
        public Label Label { get; }
    }
}