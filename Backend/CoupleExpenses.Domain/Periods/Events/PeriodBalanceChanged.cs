using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Events
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