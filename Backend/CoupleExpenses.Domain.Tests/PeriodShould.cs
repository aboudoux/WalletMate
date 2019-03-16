using System.Linq;
using CoupleExpenses.Domain.Periods;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Domain.Tests
{
    public sealed class PeriodShould 
    {
        [Fact]
        public void raise_period_created_when_create_period()
        {
            var period = Some.Period();

            period.UncommittedEvents.GetStream()
                .Should().HaveCount(1).And
                .ContainEquivalentOf(new PeriodCreated(PeriodName.From(3, 2019)), e=>e.Excluding(a=>a.AggregateId));
        }

        [Fact]
        public void raise_spending_added_when_add_spending()
        {
            var period = Some.Period();

            period.AddSpending(Amount.From(10), Label.From("Test spending"), Pair.Aurelien, SpendingOperationType.Common);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(
                    new SpendingAdded(1, 10, "Test spending", PairInfo.Aurelien, SpendingOperationTypeInfo.Common), 
                    e => e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence));
        }

        [Fact]
        public void raise_spending_removed_when_remove_existing_spending()
        {
            var period = Some.Period(p =>
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(1))));

            period.RemoveSpending(OperationId.From(1));

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new SpendingRemoved(1),
                e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void DontRaiseSpendingRemovedWhenRemoveUnexistingSpending()
        {
            var period = Some.Period(p => 
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(1))));

            period.RemoveSpending(OperationId.From(2));

            period.UncommittedEvents.GetStream().OfType<SpendingRemoved>().Should().BeEmpty();
        }

        [Fact]
        public void GenerateNewIdWhenRaisingSpendingAdded()
        {
            var period = Some.Period();

            for(var i = 1; i < 10; i++)
                period.AddSpending(Amount.From(i), Label.From("Test spending"), Pair.Aurelien, SpendingOperationType.Common);

            period.UncommittedEvents.GetStream()
                .OfType<SpendingAdded>()
                .Select(a => a.OperationId)
                .OrderBy(a => a)
                .ToList().Should().ContainInOrder(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [Fact]
        public void GenerateOperationIdBasedOnPreviousEvents()
        {
            var period = Some.Period(p =>
                p.WithEvent(Some.SpendingAdded(OperationId.From(1)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(2)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(3)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(4))));

            period.AddSpending(Amount.From(10),Label.From("test"), Pair.Aurelien, SpendingOperationType.Advance);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new SpendingAdded(5, 10, "test", PairInfo.Aurelien, SpendingOperationTypeInfo.Advance),
                    e=>e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence) );
        }

        [Fact]
        public void RaiseLabelChangedIfJustChangingLabel()
        {
            var period = ChangeSpendingInfo(label: Label.From("coucou"));

            period.UncommittedEvents.GetStream().Should().HaveCount(1).And
                .ContainEquivalentOf(new LabelChanged(OperationId.From(1), Label.From("coucou")),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseAmountChangedIfJustChangingAmount()
        {
            var period = ChangeSpendingInfo(amount: Amount.From(5));

            period.UncommittedEvents.GetStream().Should().HaveCount(1).And
                .ContainEquivalentOf(new AmountChanged(OperationId.From(1), Amount.From(5)),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaisePairChangedIfJustChangingPair()
        {
            var period = ChangeSpendingInfo(pair: Pair.Marie);

            period.UncommittedEvents.GetStream().Should().HaveCount(1).And
                .ContainEquivalentOf(new PairChanged(OperationId.From(1), Pair.Marie),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseOperationTypeChangedIfJustChangingOperationType()
        {
            var period = ChangeSpendingInfo(operationType:SpendingOperationType.Common);

            period.UncommittedEvents.GetStream().Should().HaveCount(1).And
                .ContainEquivalentOf(new SpendingOperationTypeChanged(OperationId.From(1), SpendingOperationType.Common),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseMultipleChange()
        {
            var period = ChangeSpendingInfo(Amount.From(25), Label.From("well"), Pair.Marie, SpendingOperationType.Common);
            period.UncommittedEvents.GetStream().Should().HaveCount(4);
        }

        [Fact]
        public void NotRaiseChangedEventIfDataChangedTwice()
        {
            var period = Some.Period();
            var id = period.AddSpending(Amount.From(10), Label.From("test"), Pair.Aurelien, SpendingOperationType.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingOperationType.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Aurelien, SpendingOperationType.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Marie, SpendingOperationType.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Marie, SpendingOperationType.Common);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Marie, SpendingOperationType.Common);

            period.UncommittedEvents.GetStream().Should().HaveCount(7);
        }

        [Fact]
        public void RaiseRecipeAddedWhenAddingRecipe()
        {
            var period = Some.Period();
            period.AddRecipe(Amount.From(100), Label.From("test"), Pair.Aurelien, RecipeOperationType.PartiallyDue);

            period.UncommittedEvents.GetStream().Should().HaveCount(3).And
                .ContainEquivalentOf(new RecipeAdded(1, 100, "test", PairInfo.Aurelien, RecipeOperationTypeInfo.PartiallyDue),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseRecipeRemovedWhenRemoveExistingRecipe() 
        {
            var period = Some.Period(p =>
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                    .WithEvent(Some.RecipeAdded(OperationId.From(1))));

            period.RemoveRecipe(OperationId.From(1));

            period.UncommittedEvents.GetStream().Should().HaveCount(1).And
                .ContainEquivalentOf(new RecipeRemoved(1),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }
                
        
        private static Period ChangeSpendingInfo(Amount amount = null, Label label = null, Pair pair = null, SpendingOperationType operationType = null)
        {
            var period = Some.Period(p => p.WithEvent(new SpendingAdded(1, 10, "test", PairInfo.Aurelien, SpendingOperationTypeInfo.Advance)));
            MakeChanges(period, amount, label, pair, operationType);
            return period;
        }

        private static void MakeChanges(Period period, Amount amount = null, Label label = null, Pair pair = null, SpendingOperationType operationType = null)
        {
            period.ChangeSpending(OperationId.From(1),
                amount ?? Amount.From(10),
                label ?? Label.From("test"),
                pair ?? Pair.Aurelien,
                operationType ?? SpendingOperationType.Advance);
        }
    }
}
