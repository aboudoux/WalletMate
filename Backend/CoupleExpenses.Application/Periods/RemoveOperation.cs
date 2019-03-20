using CoupleExpenses.Domain.Periods.ValueObjects;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Periods
{
    public sealed class RemoveOperation : ICommand
    {
        public PeriodId PeriodId { get; }
        public OperationId OperationId { get; }

        public RemoveOperation(PeriodId periodId, OperationId operationId)
        {
            PeriodId = periodId;
            OperationId = operationId;
        }
    }
}