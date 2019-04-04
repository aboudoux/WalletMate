using CoupleExpenses.Application.Periods.Queries;

namespace CoupleExpenses.Infrastructure.Dto
{
    public class PeriodBalance : IPeriodBalance
    {
        public PeriodBalance(double amountDue, string @by)
        {
            AmountDue = amountDue;
            By = @by;
        }

        public double AmountDue { get; }
        public string By { get; }
    }
}