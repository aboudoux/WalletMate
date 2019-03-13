using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class PeriodCreated : DomainEvent
    {
        public PeriodCreated(PeriodName periodName)
        {
            PeriodName = new PeriodNameInfos(periodName.Month, periodName.Year);
        }
        public PeriodNameInfos PeriodName { get; }
    }
}