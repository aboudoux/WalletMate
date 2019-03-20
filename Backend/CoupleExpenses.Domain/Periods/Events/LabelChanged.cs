using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("ef732ebb-fb81-472c-8e5b-dd1b91d94a09")]
    public sealed class LabelChanged : DomainEvent
    {
        public LabelChanged(OperationId operationId, Label label)
        {
            OperationId = operationId.Value;
            Label = label;
        }

        public int OperationId { get; }
        public string Label { get; }
    }
}