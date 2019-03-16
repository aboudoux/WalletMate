using System;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods
{
    internal sealed class PeriodState 
    {
        private readonly PeriodOperations _periodOperations = new PeriodOperations();
        private int _lastOperationId;

        internal void Handle(SpendingAdded @event) {
            _periodOperations.Process(@event);
            UpdateOperationIdIfGreater(@event);
        }

        internal void Handle(AmountChanged @event) => _periodOperations.Process(@event);
        internal void Handle(LabelChanged @event) => _periodOperations.Process(@event);
        internal void Handle(PairChanged @event) => _periodOperations.Process(@event);
        internal void Handle(SpendingOperationTypeChanged @event) => _periodOperations.Process(@event);

        internal void Handle(RecipeAdded @event) 
        {
            _periodOperations.Process(@event);
            UpdateOperationIdIfGreater(@event);
        }

        internal void Handle(RecipeOperationTypeChanged @event) => _periodOperations.Process(@event);

        internal void Handle(SpendingRemoved @event) => _periodOperations.Process(@event);

        internal int GetNextOperationId() => ++_lastOperationId;

        internal bool OperationExists(OperationId operationId) => _periodOperations.Exists(operationId.Value);

        private void UpdateOperationIdIfGreater(IOperation @event) {
            if (@event.OperationId > _lastOperationId)
                _lastOperationId = @event.OperationId;
        }

        internal bool LabelNotEqual(OperationId operationId, Label newLabel) => newLabel != null && _periodOperations.LabelNotEquals(operationId.Value, newLabel.Value);
        internal bool AmountNotEqual(OperationId operationId, Amount newAmount) => newAmount != null &&_periodOperations.AmountNotEquals(operationId.Value, newAmount.Value);
        internal bool PairNotEquals(OperationId operationId, Pair newPair) => newPair != null && _periodOperations.PairNotEquals(operationId.Value, (PairInfo) newPair.Value);
        internal bool OperationTypeNotEquals(OperationId operationId, SpendingOperationType operationType) => operationType != null && _periodOperations.OperationTypeNotEquals(operationId.Value, operationType.Value);
        internal bool OperationTypeNotEquals(OperationId operationId, RecipeOperationType operationType) => operationType != null && _periodOperations.OperationTypeNotEquals(operationId.Value, operationType.Value);

        internal (double amount, PairInfo by) GetBalance()
            => _periodOperations.GetBalance();
    }
}