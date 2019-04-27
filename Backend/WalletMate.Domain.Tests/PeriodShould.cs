using System.Linq;
using FluentAssertions;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Domain.Tests
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

            period.AddSpending(Amount.From(10), Label.From("Test spending"), Pair.First, SpendingCategory.Common);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(
                    new SpendingAdded(OperationId.From(1), Amount.From(10), Label.From("Test spending"), Pair.First, SpendingCategory.Common), 
                    e => e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence));
        }

        [Fact]
        public void raise_spending_removed_when_remove_existing_spending()
        {
            var period = Some.Period(p =>
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(1))));

            period.RemoveOperation(OperationId.From(1));

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new SpendingRemoved(OperationId.From(1)),
                e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void dont_raise_spending_removed_when_remove_unexisting_spending()
        {
            var period = Some.Period(p => 
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(1))));

            period.RemoveOperation(OperationId.From(2));

            period.UncommittedEvents.GetStream().OfType<SpendingRemoved>().Should().BeEmpty();
        }

        [Fact]
        public void generate_new_id_when_raising_spending_added()
        {
            var period = Some.Period();

            for(var i = 1; i < 10; i++)
                period.AddSpending(Amount.From(i), Label.From("Test spending"), Pair.First, SpendingCategory.Common);

            period.UncommittedEvents.GetStream()
                .OfType<SpendingAdded>()
                .Select(a => a.OperationId.Value)
                .OrderBy(a => a)
                .ToList().Should().ContainInOrder(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [Fact]
        public void Generate_operationId_based_on_previous_events()
        {
            var period = Some.Period(p =>
                p.WithEvent(Some.SpendingAdded(OperationId.From(1)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(2)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(3)))
                 .WithEvent(Some.SpendingAdded(OperationId.From(4))));

            period.AddSpending(Amount.From(10),Label.From("test"), Pair.First, SpendingCategory.Advance);

            period.UncommittedEvents.GetStream().Should()
                .ContainEquivalentOf(new SpendingAdded(OperationId.From(5), Amount.From(10), Label.From("test"), Pair.First, SpendingCategory.Advance),
                    e=>e.Excluding(a=>a.AggregateId).Excluding(a=>a.Sequence) );
        }

        [Fact]
        public void raise_label_changed_if_just_changing_label()
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

            period.UncommittedEvents.GetStream().Should().HaveCount(2).And
                .ContainEquivalentOf(new AmountChanged(OperationId.From(1), Amount.From(5)),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaisePairChangedIfJustChangingPair()
        {
            var period = ChangeSpendingInfo(pair: Pair.Second);

            period.UncommittedEvents.GetStream().Should().HaveCount(2).And
                .ContainEquivalentOf(new PairChanged(OperationId.From(1), Pair.Second),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void Raise_CategoryChanged_if_just_changing_category()
        {
            var period = ChangeSpendingInfo(category:SpendingCategory.Common);

            period.UncommittedEvents.GetStream().Should().HaveCount(2).And
                .ContainEquivalentOf(new SpendingCategoryChanged(OperationId.From(1), SpendingCategory.Common),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseMultipleChange()
        {
            var period = ChangeSpendingInfo(Amount.From(25), Label.From("well"), Pair.Second, SpendingCategory.Common);
            period.UncommittedEvents.GetStream().Should().HaveCount(5);
        }

        [Fact]
        public void NotRaiseChangedEventIfDataChangedTwice()
        {
            var period = Some.Period();
            var id = period.AddSpending(Amount.From(10), Label.From("test"), Pair.First, SpendingCategory.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test"), Pair.First, SpendingCategory.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.First, SpendingCategory.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Second, SpendingCategory.Advance);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Second, SpendingCategory.Common);
            period.ChangeSpending(id, Amount.From(5), Label.From("test2"), Pair.Second, SpendingCategory.Common);

            period.UncommittedEvents.GetStream().Should().HaveCount(12);
        }

        [Fact]
        public void RaiseRecipeAddedWhenAddingRecipe()
        {
            var period = Some.Period();
            period.AddRecipe(Amount.From(100), Label.From("test"), Pair.First, RecipeCategory.Common);

            period.UncommittedEvents.GetStream().Should().HaveCount(3).And
                .ContainEquivalentOf(new RecipeAdded(OperationId.From(1), Amount.From(100), Label.From("test"), Pair.First, RecipeCategory.Common),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }

        [Fact]
        public void RaiseRecipeRemovedWhenRemoveExistingRecipe() 
        {
            var period = Some.Period(p =>
                p.WithEvent(new PeriodCreated(PeriodName.From(1, 2019)))
                    .WithEvent(Some.RecipeAdded(OperationId.From(1))));

            period.RemoveOperation(OperationId.From(1));

            period.UncommittedEvents.GetStream().Should().HaveCount(2).And
                .ContainEquivalentOf(new RecipeRemoved(OperationId.From(1)),
                    e => e.Excluding(a => a.AggregateId).Excluding(a => a.Sequence));
        }
                
        
        private static Period ChangeSpendingInfo(Amount amount = null, Label label = null, Pair pair = null, SpendingCategory category = null)
        {
            var period = Some.Period(p => p.WithEvent(new SpendingAdded(OperationId.From(1), Amount.From(10), Label.From("test"), Pair.First, SpendingCategory.Advance)));
            MakeChanges(period, amount, label, pair, category);
            return period;
        }

        private static void MakeChanges(Period period, Amount amount = null, Label label = null, Pair pair = null, SpendingCategory category = null)
        {
            period.ChangeSpending(OperationId.From(1),
                amount ?? Amount.From(10),
                label ?? Label.From("test"),
                pair ?? Pair.First,
                category ?? SpendingCategory.Advance);
        }
    }
}
