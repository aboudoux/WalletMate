using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods
{
    public sealed class AddRecipe : ICommand
    {
        public PeriodId PeriodId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public RecipeOperationType OperationType { get; }

        public AddRecipe(PeriodId periodId, Amount amount, Label label, Pair pair, RecipeOperationType operationType)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            OperationType = operationType;
        }        
    }
}