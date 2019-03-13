using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Domain.Tests
{
    public class PeriodBalanceChangedShould
    {
        [Fact]
        public void be_raised_when_adding_spending_operation()
        {
            var period = Some.Period();
            period.AddSpending(Amount.From(100), Label.From("Test depense"), Pair.Aurelien, SpendingOperationType.Common);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new PeriodBalanceChanged(50, PairInfo.Marie), 
                    e => e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence));
        }        
    }
}