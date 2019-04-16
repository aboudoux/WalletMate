using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods
{
    public sealed class AddRecipe : ICommand
    {
        public PeriodId PeriodId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public RecipeCategory Category { get; }

        public AddRecipe(PeriodId periodId, Amount amount, Label label, Pair pair, RecipeCategory category)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            Category = category;
        }        
    }
}