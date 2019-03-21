using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
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