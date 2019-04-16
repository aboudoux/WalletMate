using WalletMate.Application.Periods.Queries;

namespace WalletMate.Infrastructure.Dto
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