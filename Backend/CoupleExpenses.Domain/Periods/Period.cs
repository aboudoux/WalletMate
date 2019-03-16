using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods 
{
    public sealed class Period : AggregateRoot 
    {
        private readonly PeriodState _state = new PeriodState();
             
        public Period(History history)
        {            
            AddPlayer<SpendingAdded>(e => _state.Handle(e));
            AddPlayer<SpendingRemoved>(e => _state.Handle(e));
            AddPlayer<AmountChanged>(e => _state.Handle(e));
            AddPlayer<LabelChanged>(e => _state.Handle(e));
            AddPlayer<PairChanged>(e => _state.Handle(e));
            AddPlayer<RecipeAdded>(e=>_state.Handle(e));
            AddPlayer<SpendingOperationTypeChanged>(e => _state.Handle(e));
            AddPlayer<RecipeOperationTypeChanged>(e => _state.Handle(e));
            HydrateFrom(history);
        }

        public static Period Create(PeriodName periodName) 
            => CreateNew<Period>(PeriodId.New().Value, new PeriodCreated(periodName));

        public OperationId AddSpending(Amount amount, Label label, Pair pair, SpendingOperationType operationType)
        {
            var operationId = OperationId.From(_state.GetNextOperationId());
            RaiseEvent(new SpendingAdded(operationId.Value, amount.Value, label.Value, (PairInfo) pair.Value, (SpendingOperationTypeInfo) operationType.Value));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeSpending(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null,
            SpendingOperationType operationType = null)
        {
            ChangeOperation(operationId, amount, label, pair, operationType);
            RaiseBalanceChanged();
        }

        public void RemoveSpending(OperationId operationId)
        {
            if(_state.OperationExists(operationId))
                RaiseEvent(new SpendingRemoved(operationId.Value));
        }

        public OperationId AddRecipe(Amount amount, Label label, Pair pair, RecipeOperationType operationType)
        {
            var operationId = OperationId.From(_state.GetNextOperationId());
            RaiseEvent(new RecipeAdded(operationId.Value, amount.Value, label.Value, (PairInfo) pair.Value, (RecipeOperationTypeInfo) operationType.Value));
            RaiseBalanceChanged();
            return operationId;
        }

        public void ChangeRecipe(OperationId operationId, Amount amount = null, Label label = null, Pair pair = null,
            RecipeOperationType operationType = null)
        {
            ChangeOperation(operationId, amount, label, pair, operationType);
            RaiseBalanceChanged();
        }

        public void RemoveRecipe(OperationId operationId)
        {
            if (_state.OperationExists(operationId))
                RaiseEvent(new RecipeRemoved(operationId.Value));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, SpendingOperationType operationType)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (_state.OperationTypeNotEquals(operationId, operationType))
                RaiseEvent(new SpendingOperationTypeChanged(operationId, operationType));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair, RecipeOperationType operationType)
        {
            ChangeOperation(operationId, amount, label, pair);
            if (_state.OperationTypeNotEquals(operationId, operationType))
                RaiseEvent(new RecipeOperationTypeChanged(operationId, operationType));
        }

        private void ChangeOperation(OperationId operationId, Amount amount, Label label, Pair pair)
        {
            if (_state.LabelNotEqual(operationId, label))
                RaiseEvent(new LabelChanged(operationId, label));
            if (_state.AmountNotEqual(operationId, amount))
                RaiseEvent(new AmountChanged(operationId, amount));
            if (_state.PairNotEquals(operationId, pair))
                RaiseEvent(new PairChanged(operationId, pair));
        }

        private void RaiseBalanceChanged()
        {
            var balance = _state.GetBalance();
            RaiseEvent(new PeriodBalanceChanged(balance.amount, balance.by));
        }
    }
}
