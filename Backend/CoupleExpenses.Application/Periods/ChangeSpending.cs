using CoupleExpenses.Domain.Periods.ValueObjects;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Periods
{
    public sealed class ChangeSpending : ICommand
    {
        public PeriodId PeriodId { get; }
        public OperationId OperationId { get; }
        public Amount Amount { get; }
        public Label Label { get; }
        public Pair Pair { get; }
        public SpendingOperationType OperationType { get; }

        public ChangeSpending(PeriodId periodId, OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, SpendingOperationType operationType = null)
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