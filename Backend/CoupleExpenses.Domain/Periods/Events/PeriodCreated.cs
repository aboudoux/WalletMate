using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("PeriodCreated")]
    public sealed class PeriodCreated : DomainEvent
    {
        public PeriodCreated(PeriodName periodName)
        {
            PeriodName = periodName;
        }

        public PeriodName PeriodName { get; }
    }
}