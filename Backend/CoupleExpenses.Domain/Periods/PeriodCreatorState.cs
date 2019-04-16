using System.Collections.Generic;
using System.Linq;
using WalletMate.Domain.Common;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods
{
    public sealed class PeriodCreatorState : AggregateState
    {
        private readonly SortedSet<PeriodName> _allPeriodNames = new SortedSet<PeriodName>(new PeriodNameComparer());

        public PeriodCreatorState()
        {
            AddHandler<PeriodCreated>(Handle);
        }
        internal void Handle(PeriodCreated @event)
        {
            _allPeriodNames.Add(PeriodName.From(@event.PeriodName.Month, @event.PeriodName.Year));
        }

        internal bool PeriodExists(PeriodName periodName) => _allPeriodNames.Contains(periodName);

        internal PeriodName LastPeriod => _allPeriodNames.Last();
    }
}