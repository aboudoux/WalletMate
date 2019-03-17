using System.Collections.Generic;
using System.Linq;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods
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