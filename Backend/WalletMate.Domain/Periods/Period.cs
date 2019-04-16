using System.Linq;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods 
{
    public sealed class Period : AggregateRoot<PeriodState>
    {             
        public Period(History history) : base(history)
        {
        }

        public PeriodName PeriodName => State.PeriodName;

        public static Period Create(PeriodName periodName) 
            => CreateNew<Period>(PeriodId.From(periodName).Value, new PeriodCreated(periodName));

        public OperationId AddSpending(Amount amount, Label label, Pair pair, SpendingCategory category)
        {
            var operationId = OperationId.From(State.GetNextOperationId());
            RaiseEvent(new SpendingAdded(operationId, amount, label, pair, category));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeSpending(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, SpendingCategory category = null)
        {
            ChangeOperation(operationId, amount, label, pair, category);
            if (UncommitedEventsHaveDifferentEventThatLabelChanged())
                RaiseBalanceChanged();
        }
      
        public OperationId AddRecipe(Amount amount, Label label, Pair pair, RecipeCategory category)
        {
            var operationId = OperationId.From(State.GetNextOperationId());
            RaiseEvent(new RecipeAdded(operationId, amount, label, pair, category));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeRecipe(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, RecipeCategory category = null)
        {
            ChangeOperation(operationId, amount, label, pair, category);

            if(UncommitedEventsHaveDifferentEventThatLabelChanged())
                RaiseBalanceChanged();            
        }

        public void RemoveOperation(OperationId operationId)
        {            
            if (!State.OperationExists(operationId)) return;

            if (State.IsSpendingOperation(operationId))
                RaiseEvent(new SpendingRemoved(operationId));
            else
                RaiseEvent(new RecipeRemoved(operationId));
            RaiseBalanceChanged();
        }


        private bool UncommitedEventsHaveDifferentEventThatLabelChanged()
            => UncommittedEvents.GetStream().Any(a => a.GetType() != typeof(LabelChanged));

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, SpendingCategory category)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (State.CategoryNotEquals(operationId, category))
                RaiseEvent(new SpendingCategoryChanged(operationId, category));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, RecipeCategory category)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (State.CategoryNotEquals(operationId, category))
                RaiseEvent(new RecipeCategoryChanged(operationId, category));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair)
        {
            if (State.LabelNotEqual(operationId, label))
                RaiseEvent(new LabelChanged(operationId, label));
            if (State.AmountNotEqual(operationId, amount))
                RaiseEvent(new AmountChanged(operationId, amount));
            if (State.PairNotEquals(operationId, pair))
                RaiseEvent(new PairChanged(operationId, pair));
        }

        private void RaiseBalanceChanged()
        {
            var balance = State.ComputeBalance();
            RaiseEvent(new PeriodBalanceChanged(balance.amount, balance.by));
        }
    }
}
