using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Events
{
    [SerializableTypeIdentifier("80317c9e-ff5d-469f-b24a-c936c17e5dde")]
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