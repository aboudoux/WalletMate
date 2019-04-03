using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Periods 
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
