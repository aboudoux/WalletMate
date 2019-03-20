using System;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("8a187ae9-e23e-4c58-a8c0-fe74e779f23c")]
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