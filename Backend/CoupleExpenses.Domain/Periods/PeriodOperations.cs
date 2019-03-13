using System;
using System.Collections.Generic;
using CoupleExpenses.Domain.Periods.Events;

namespace CoupleExpenses.Domain.Periods
{
    public sealed class PeriodOperations
    {
        private readonly Dictionary<int, OperationDto> _allOperations = new Dictionary<int, OperationDto>();

        public void Process(SpendingAdded @event) => _allOperations.Add(@event.OperationId, new OperationDto(@event));
        public void Process(AmountChanged @event) => _allOperations[@event.OperationId].Amount = @event.Amount;
        public void Process(LabelChanged @event) => _allOperations[@event.OperationId].Label = @event.Label;
        public void Process(PairChanged @event) => _allOperations[@event.OperationId].Pair = (PairInfo)@event.Pair;
        public void Process(SpendingOperationTypeChanged @event) => _allOperations[@event.OperationId].OperationType = @event.OperationType;
        public void Process(RecipeOperationTypeChanged @event) => _allOperations[@event.OperationId].OperationType = @event.OperationType;

        internal void Process(RecipeAdded @event) => _allOperations.Add(@event.OperationId, new OperationDto(@event));

        public void Process(SpendingRemoved @event) => _allOperations.Remove(@event.OperationId);

        public bool LabelNotEquals(int operationId, string newLabel) => _allOperations[operationId].Label != newLabel;
        public bool AmountNotEquals(int operationId, double newAmount) => Math.Abs(_allOperations[operationId].Amount - newAmount) > double.Epsilon;
        public bool PairNotEquals(int operationId, PairInfo pairInfo) => _allOperations[operationId].Pair != pairInfo;
        public bool OperationTypeNotEquals(int operationId, int operationType) => _allOperations[operationId].OperationType != operationType;

        public bool Exists(int operationId) => _allOperations.ContainsKey(operationId);
    }

    public sealed class OperationDto
    {
       public OperationDto(SpendingAdded @event)
        {
            OperationId = @event.OperationId;
            Label = @event.Label;
            Amount = @event.Amount;
            Pair = @event.Pair;
            OperationType = (int)@event.Type;
            IsSpending = true;
        }

        public OperationDto(RecipeAdded @event) {
            OperationId = @event.OperationId;
            Label = @event.Label;
            Amount = @event.Amount;
            Pair = @event.Pair;
            OperationType = (int)@event.Type;
            IsSpending = false;
        }

        public bool IsSpending { get; }
      
        public int OperationId { get; set; }
        public string Label { get; set; }
        public double Amount { get; set; }
        public PairInfo Pair { get; set; }
        public int OperationType { get; set; }        
    }
}