using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods
{
    public sealed class AddSpending : ICommand
    {
        public PeriodId PeriodId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public SpendingCategory Category { get; }

        public AddSpending(PeriodId periodId, Amount amount, Label label, Pair pair, SpendingCategory category)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            Category = category;
        }
    }   
}