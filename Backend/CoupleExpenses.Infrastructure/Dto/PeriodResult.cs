using CoupleExpenses.Application.Periods.Queries;

namespace CoupleExpenses.Infrastructure.Dto
{
    public class PeriodResult : IPeriodResult
    {
        public PeriodResult(string periodName, string periodId)
        {
            PeriodName = periodName;
            PeriodId = periodId;
        }

        public string PeriodName { get; }
        public string PeriodId { get; }
    }
}