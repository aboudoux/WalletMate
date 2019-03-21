using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("PeriodBalanceChanged")]
    public sealed class PeriodBalanceChanged : DomainEvent
    {
        public PeriodBalanceChanged(Amount amountDue, Pair @by)
        {
            AmountDue = amountDue;
            By = @by;
        }

        public Amount AmountDue {get;}

        public Pair By {get;}
    }
}