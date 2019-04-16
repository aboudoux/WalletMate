using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods.Queries
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