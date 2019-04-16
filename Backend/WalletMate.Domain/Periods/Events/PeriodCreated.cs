using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
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