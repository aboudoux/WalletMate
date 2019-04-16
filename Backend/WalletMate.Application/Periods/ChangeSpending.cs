using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods
{
    public sealed class ChangeSpending : ICommand
    {
        public PeriodId PeriodId { get; }
        public OperationId OperationId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public SpendingCategory Category { get; }

        public ChangeSpending(PeriodId periodId, OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, SpendingCategory category = null)
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