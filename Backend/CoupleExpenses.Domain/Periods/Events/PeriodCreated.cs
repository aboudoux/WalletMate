using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("38cf0281-9790-4608-b74f-63fa6da2eee9")]
    public sealed class PeriodCreated : DomainEvent
    {
        public PeriodCreated(PeriodName periodName)
        {
            PeriodName = periodName;
        }

        public PeriodName PeriodName { get; }
    }
}