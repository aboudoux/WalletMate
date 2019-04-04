using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods.Queries
{
    public class GetPeriodBalance : IQuery<IPeriodBalance>
    {
        public PeriodId PeriodId { get; }

        public GetPeriodBalance(PeriodId periodId)
        {
            PeriodId = periodId;
        }
    }

    public interface IPeriodBalance
    {
        double AmountDue { get; }
        string By { get; }
    }
}