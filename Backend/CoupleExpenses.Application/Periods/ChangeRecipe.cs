using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods
{
    public sealed class ChangeRecipe : ICommand
    {
        public PeriodId PeriodId { get; }
        public OperationId OperationId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public RecipeCategory Category { get; }

        public ChangeRecipe(PeriodId periodId, OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, RecipeCategory category = null)
        {
            PeriodId = periodId;
            OperationId = operationId;
            Amount = amount;
            Label = label;
            Pair = pair;
            Category = category;
        }
    }
}