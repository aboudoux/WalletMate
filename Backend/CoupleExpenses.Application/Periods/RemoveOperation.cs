using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods
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