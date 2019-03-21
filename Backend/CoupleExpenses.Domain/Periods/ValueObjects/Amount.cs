using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("Amount")]
    public class Amount : PositiveDoubleValueObject<Amount>
    {
        private Amount(double value) : base(value)
        {
        }

        [JsonConstructor]
        private Amount(double value, bool deserialization = true) : base(value, deserialization)
        {
        }
    }
}