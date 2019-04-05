using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods
{
    public sealed class ChangeRecipe : ICommand
    {
        public PeriodId PeriodId { get; }
        public OperationId OperationId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public RecipeOperationType OperationType { get; }

        public ChangeRecipe(PeriodId periodId, OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, RecipeOperationType operationType = null)
        {
            PeriodId = periodId;
            OperationId = operationId;
            Amount = amount;
            Label = label;
            Pair = pair;
            OperationType = operationType;
        }
    }
}