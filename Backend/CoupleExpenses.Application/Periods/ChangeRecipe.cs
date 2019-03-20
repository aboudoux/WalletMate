using CoupleExpenses.Domain.Periods.ValueObjects;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Periods
{
    public sealed class ChangeRecipe : ICommand
    {
        public PeriodId PeriodId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public RecipeOperationType OperationType { get; }

        public ChangeRecipe(PeriodId periodId, Amount amount = null, Label label = null, Pair pair = null, RecipeOperationType operationType = null)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            OperationType = operationType;
        }
    }
}