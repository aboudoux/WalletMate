using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;

namespace CoupleExpenses.Domain.Periods.Events
{
    public sealed class PeriodBalanceChanged : DomainEvent
    {
        public PeriodBalanceChanged(double amountDue, PairInfo @by)
        {
            AmountDue = amountDue;
            By = @by;
        }

        public double AmountDue{get;}

        public PairInfo By {get;}
    }
}