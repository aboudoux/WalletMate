using System;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application 
{
    public class CreatePeriod        
    {
        public PeriodName PeriodName { get; }

        public CreatePeriod(PeriodName periodName)
        {
            PeriodName = periodName;
        }
    }

    public class PeriodCommandHandler
    {

    }
}
