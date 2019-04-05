using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods
{
    public sealed class PeriodState : AggregateState
    {
        private readonly PeriodOperations _periodOperations = new PeriodOperations();
        private int _lastOperationId;

        public PeriodState()
        {
            AddHandler<PeriodCreated>(e => PeriodName = PeriodName.From(e.PeriodName.Month, e.PeriodName.Year));
            AddHandler<SpendingAdded>(Handle);
            AddHandler<AmountChanged>(Handle);
            AddHandler<LabelChanged>(Handle);
            AddHandler<PairChanged>(Handle);
            AddHandler<SpendingCategoryChanged>(Handle);
            AddHandler<RecipeAdded>(Handle);
            AddHandler<RecipeCategoryChanged>(Handle);
            AddHandler<SpendingRemoved>(Handle);
            AddHandler<RecipeRemoved>(Handle);
        }

        internal void Handle(SpendingAdded @event) {
            _periodOperations.Process(@event);
            UpdateOperationIdIfGreater(@event);
        }
        internal void Handle(AmountChanged @event) => _periodOperations.Process(@event);
        internal void Handle(LabelChanged @event) => _periodOperations.Process(@event);
        internal void Handle(PairChanged @event) => _periodOperations.Process(@event);
        internal void Handle(SpendingCategoryChanged @event) => _periodOperations.Process(@event);
        internal void Handle(RecipeAdded @event) 
        {
            _periodOperations.Process(@event);
            UpdateOperationIdIfGreater(@event);
        }
        internal void Handle(RecipeCategoryChanged @event) => _periodOperations.Process(@event);
        internal void Handle(SpendingRemoved @event) => _periodOperations.Process(@event);
        internal void Handle(RecipeRemoved @event) => _periodOperations.Process(@event);

        internal int GetNextOperationId() => ++_lastOperationId;

        internal bool OperationExists(OperationId operationId) => _periodOperations.Exists(operationId.Value);

        internal bool IsSpendingOperation(OperationId operationId) => _periodOperations.IsSpendingOperation(operationId.Value);

        private void UpdateOperationIdIfGreater(IOperation @event)
        {
            if (@event.OperationId.Value > _lastOperationId)
                _lastOperationId = @event.OperationId.Value;
        }

        internal bool LabelNotEqual(OperationId operationId, Label newLabel) => newLabel != null && _periodOperations.LabelNotEquals(operationId.Value, newLabel.Value);
        internal bool AmountNotEqual(OperationId operationId, Amount newAmount) => newAmount != null &&_periodOperations.AmountNotEquals(operationId.Value, newAmount.Value);
        internal bool PairNotEquals(OperationId operationId, Pair newPair) => newPair != null && _periodOperations.PairNotEquals(operationId.Value, newPair);
        internal bool CategoryNotEquals(OperationId operationId, SpendingCategory category) => category != null && _periodOperations.CategoryNotEquals(operationId.Value, category.Value);
        internal bool CategoryNotEquals(OperationId operationId, RecipeCategory category) => category != null && _periodOperations.CategoryNotEquals(operationId.Value, category.Value);

        internal (Amount amount, Pair by) ComputeBalance()
            => _periodOperations.ComputeBalance();

        public PeriodName PeriodName { get; private set; }
    }
}