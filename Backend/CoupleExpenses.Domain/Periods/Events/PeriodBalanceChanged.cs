using CoupleExpenses.Domain.Common.Events;

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