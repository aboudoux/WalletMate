using System.Linq;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods 
{
    public sealed class Period : AggregateRoot<PeriodState>
    {             
        public Period(History history) : base(history)
        {
        }

        public PeriodName PeriodName => State.PeriodName;

        public static Period Create(PeriodName periodName) 
            => CreateNew<Period>(PeriodId.New().Value.ToString(), new PeriodCreated(periodName));

        public OperationId AddSpending(Amount amount, Label label, Pair pair, SpendingOperationType operationType)
        {
            var operationId = OperationId.From(State.GetNextOperationId());
            RaiseEvent(new SpendingAdded(operationId, amount, label, pair, operationType));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeSpending(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, SpendingOperationType operationType = null)
        {
            ChangeOperation(operationId, amount, label, pair, operationType);
            if (UncommitedEventsHaveDifferentEventThatLabelChanged())
                RaiseBalanceChanged();
        }
      
        public OperationId AddRecipe(Amount amount, Label label, Pair pair, RecipeOperationType operationType)
        {
            var operationId = OperationId.From(State.GetNextOperationId());
            RaiseEvent(new RecipeAdded(operationId, amount, label, pair, operationType));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeRecipe(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, RecipeOperationType operationType = null)
        {
            ChangeOperation(operationId, amount, label, pair, operationType);

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

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, SpendingOperationType operationType)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (State.OperationTypeNotEquals(operationId, operationType))
                RaiseEvent(new SpendingOperationTypeChanged(operationId, operationType));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, RecipeOperationType operationType)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (State.OperationTypeNotEquals(operationId, operationType))
                RaiseEvent(new RecipeOperationTypeChanged(operationId, operationType));
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
