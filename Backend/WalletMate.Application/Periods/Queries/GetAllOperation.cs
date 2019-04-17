using System.Collections.Generic;
using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods.Queries
{
    public class GetAllOperation : IQuery<IReadOnlyList<IPeriodOperation>>
    {
        public GetAllOperation(PeriodId periodId)
        {
            PeriodId = periodId;
        }

        public PeriodId PeriodId { get; }
    }

    public interface IPeriodOperation
    {
        int OperationId { get; }
        string Type { get; }
        string PeriodId { get; }
        double Amount { get; set; }
        string Label { get; set; }
        string Pair { get; set; }
        int PairValue { get; set; }
        string Category { get; set; }
        int CategoryValue { get; set; }
    }
}