using FluentAssertions;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Domain.Tests
{
    public class PeriodBalanceChangedShould
    {
        [Fact]
        public void be_raised_when_adding_spending_operation()
        {
            var period = Some.Period();
            period.AddSpending(Amount.From(100), Label.From("Test depense"), Pair.First, SpendingCategory.Common);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new PeriodBalanceChanged(Amount.From(50), Pair.Second), 
                    e => e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence));
        }        
    }
}