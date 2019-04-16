using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.Exceptions;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods
{
    public sealed class PeriodCreator : AggregateRoot<PeriodCreatorState>
    {
        public PeriodCreator(History history) : base(history)
        {
        }

        public Period Create(PeriodName periodName)
        {
            return CreatePeriod(periodName);
        }

        public Period CreateNext()
        {
            var nextPeriodName = State.LastPeriod.GetIncrement();
            return CreatePeriod(nextPeriodName);
        }

        private Period CreatePeriod(PeriodName periodName)
        {
            if (State.PeriodExists(periodName))
                throw new PeriodAlreadyExistsException(periodName);

            var period = Period.Create(periodName);
            Apply(period.UncommittedEvents.GetStream());
            return period;
        }
    }
}