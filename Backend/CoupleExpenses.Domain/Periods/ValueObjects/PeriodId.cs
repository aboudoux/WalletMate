using System;
using CoupleExpenses.Domain.Common.ValueObjects;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class PeriodId : GuidValueObject<PeriodId>
    {        
        private PeriodId(Guid value) : base(value)
        {
        }
    }
}