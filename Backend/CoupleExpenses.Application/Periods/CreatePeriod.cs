using CoupleExpenses.Domain.Periods.ValueObjects;
using Mediator.Net.Contracts;

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
