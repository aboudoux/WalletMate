using System;
using System.Collections.Generic;
using System.Linq;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods
{
    public sealed class PeriodOperations
    {
        private readonly Dictionary<int, OperationDto> _allOperations = new Dictionary<int, OperationDto>();

        internal void Process(SpendingAdded @event) => _allOperations.Add(@event.OperationId.Value, new OperationDto(@event));
        internal void Process(AmountChanged @event) => _allOperations[@event.OperationId.Value].Amount = @event.Amount.Value;
        internal void Process(LabelChanged @event) => _allOperations[@event.OperationId.Value].Label = @event.Label;
        internal void Process(PairChanged @event) => _allOperations[@event.OperationId.Value].Pair = @event.Pair.Value;
        internal void Process(SpendingCategoryChanged @event) => _allOperations[@event.OperationId.Value].Category = @event.Category.Value;
        internal void Process(RecipeCategoryChanged @event) => _allOperations[@event.OperationId.Value].Category = @event.Category.Value;

        internal void Process(RecipeAdded @event) => _allOperations.Add(@event.OperationId.Value, new OperationDto(@event));

        internal void Process(SpendingRemoved @event) => _allOperations.Remove(@event.OperationId.Value);
        internal void Process(RecipeRemoved @event) => _allOperations.Remove(@event.OperationId.Value);

        internal bool LabelNotEquals(int operationId, string newLabel) => _allOperations[operationId].Label != newLabel;
        internal bool AmountNotEquals(int operationId, double newAmount) => Math.Abs(_allOperations[operationId].Amount - newAmount) > double.Epsilon;
        internal bool PairNotEquals(int operationId, Pair pairInfo) => _allOperations[operationId].Pair != pairInfo.Value;
        internal bool CategoryNotEquals(int operationId, int category) => _allOperations[operationId].Category != category;

        internal bool Exists(int operationId) => _allOperations.ContainsKey(operationId);

        internal (Amount amount, Pair by) ComputeBalance()
        {
            var totalSpending = _allOperations.Values
                .Where(a => a.IsSpending)
                .Select(a => new
                {
                    Amount = a.Category == SpendingCategory.Advance.Value
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
                    Amount = a.Category == RecipeCategory.Individual.Value
                        ? a.Amount
                        : a.Amount / 2,
                    By = a.Pair
                }).GroupBy(a => a.By,
                    a => a.Amount,
                    (key, g) => new { Amount = g.Sum(), By = key })
                .ToList();

            var amountSpendingMarie = totalSpending.FirstOrDefault(a => a.By == Pair.Marie.Value)?.Amount ?? 0;
            var amountSpendingAurelien = totalSpending.FirstOrDefault(a => a.By == Pair.Aurelien.Value)?.Amount ?? 0;

            var amountRecipeMarie = totalRecipe.FirstOrDefault(a => a.By == Pair.Marie.Value)?.Amount ?? 0;
            var amountRecipeAurelien = totalRecipe.FirstOrDefault(a => a.By == Pair.Aurelien.Value)?.Amount ?? 0;

            var amountDue =Math.Round((amountSpendingAurelien+amountRecipeMarie) - (amountSpendingMarie+amountRecipeAurelien), 2, MidpointRounding.ToEven);
            
            return (Amount.From(Math.Abs(amountDue)), amountDue < 0 ? Pair.Aurelien : Pair.Marie);
        }

        public bool IsSpendingOperation(int operationIdValue) => _allOperations[operationIdValue].IsSpending;

    }

    public sealed class OperationDto
    {
       public OperationDto(SpendingAdded @event)
        {
            OperationId = @event.OperationId.Value;
            Label = @event.Label.Value;
            Amount = @event.Amount.Value;
            Pair = @event.Pair.Value;
            Category = @event.Category.Value;
            IsSpending = true;
        }

        public OperationDto(RecipeAdded @event) {
            OperationId = @event.OperationId.Value;
            Label = @event.Label;
            Amount = @event.Amount.Value;
            Pair = @event.Pair.Value;
            Category = @event.Category.Value;
            IsSpending = false;
        }

        public bool IsSpending { get; }
      
        public int OperationId { get; set; }
        public string Label { get; set; }
        public double Amount { get; set; }
        public int Pair { get; set; }
        public int Category { get; set; }        
    }
}