using System;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("PeriodId")]
    public class PeriodId : GuidValueObject<PeriodId>
    {        
        private PeriodId(Guid value) : base(value)
        {
        }

        [JsonConstructor]
        private PeriodId(Guid value, bool _ = true) : base(value, _) {
        }
    }
}