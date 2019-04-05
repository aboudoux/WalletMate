using System.Collections.Generic;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods.Queries
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
        string OperationType { get; set; }
    }
}