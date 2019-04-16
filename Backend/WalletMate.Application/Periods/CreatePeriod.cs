using WalletMate.Application.Core;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Application.Periods 
{
    public sealed class CreatePeriod : ICommand
    {
        public PeriodName PeriodName { get; }

        public CreatePeriod(PeriodName periodName)
        {
            PeriodName = periodName;
        }
    }
}
