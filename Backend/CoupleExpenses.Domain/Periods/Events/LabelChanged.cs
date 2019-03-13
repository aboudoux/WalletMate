using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
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