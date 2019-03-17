using System;
using System.Collections.Generic;
using System.Linq;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods
{
    public sealed class PeriodOperations
    {
        private readonly Dictionary<int, OperationDto> _allOperations = new Dictionary<int, OperationDto>();

        internal void Process(SpendingAdded @event) => _allOperations.Add(@event.OperationId, new OperationDto(@event));
        internal void Process(AmountChanged @event) => _allOperations[@event.OperationId].Amount = @event.Amount;
        internal void Process(LabelChanged @event) => _allOperations[@event.OperationId].Label = @event.Label;
        internal void Process(PairChanged @event) => _allOperations[@event.OperationId].Pair = (PairInfo)@event.Pair;
        internal void Process(SpendingOperationTypeChanged @event) => _allOperations[@event.OperationId].OperationType = @event.OperationType;
        internal void Process(RecipeOperationTypeChanged @event) => _allOperations[@event.OperationId].OperationType = @event.OperationType;

        internal void Process(RecipeAdded @event) => _allOperations.Add(@event.OperationId, new OperationDto(@event));

        internal void Process(SpendingRemoved @event) => _allOperations.Remove(@event.OperationId);
        internal void Process(RecipeRemoved @event) => _allOperations.Remove(@event.OperationId);

        internal bool LabelNotEquals(int operationId, string newLabel) => _allOperations[operationId].Label != newLabel;
        internal bool AmountNotEquals(int operationId, double newAmount) => Math.Abs(_allOperations[operationId].Amount - newAmount) > double.Epsilon;
        internal bool PairNotEquals(int operationId, PairInfo pairInfo) => _allOperations[operationId].Pair != pairInfo;
        internal bool OperationTypeNotEquals(int operationId, int operationType) => _allOperations[operationId].OperationType != operationType;

        internal bool Exists(int operationId) => _allOperations.ContainsKey(operationId);

        internal (double amount, PairInfo by) ComputeBalance()
        {
            var totalSpending = _allOperations.Values
                .Where(a => a.IsSpending)
                .Select(a => new
                {
                    Amount = a.OperationType == (int) SpendingOperationTypeInfo.Advance
                        ? a.Amount
                        : a.Amount / 2,
                    By = a.Pair
                }).GroupBy(a => a.By,
                    a => a.Amount, 
                    (key, g) => new {Amount = g.Sum(), By = key})
                    .ToList();

            var totalRecipe = _allOperations.Values
                .Where(a => !a.IsSpending)
                .Select(a => new
                {
                    Amount = a.OperationType == (int)RecipeOperationTypeInfo.Individual
                        ? a.Amount
                        : a.Amount / 2,
                    By = a.Pair
                }).GroupBy(a => a.By,
                    a => a.Amount,
                    (key, g) => new { Amount = g.Sum(), By = key })
                .ToList();

            var amountSpendingMarie = totalSpending.FirstOrDefault(a => a.By == PairInfo.Marie)?.Amount ?? 0;
            var amountSpendingAurelien = totalSpending.FirstOrDefault(a => a.By == PairInfo.Aurelien)?.Amount ?? 0;

            var amountRecipeMarie = totalRecipe.FirstOrDefault(a => a.By == PairInfo.Marie)?.Amount ?? 0;
            var amountRecipeAurelien = totalRecipe.FirstOrDefault(a => a.By == PairInfo.Aurelien)?.Amount ?? 0;

            var amountDue = (amountSpendingAurelien+amountRecipeMarie) - (amountSpendingMarie+amountRecipeAurelien);
            
            return (Math.Abs(amountDue), amountDue < 0 ? PairInfo.Aurelien : PairInfo.Marie);
        }

        public bool IsSpendingOperation(int operationIdValue) => _allOperations[operationIdValue].IsSpending;

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