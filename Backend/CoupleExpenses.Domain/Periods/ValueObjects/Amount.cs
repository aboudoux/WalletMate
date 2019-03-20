using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("0edd46cd-af7d-4936-8c7f-dcdef7a8ff81")]
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