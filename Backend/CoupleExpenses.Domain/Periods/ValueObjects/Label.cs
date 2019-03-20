using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("c3af3828-62a8-4fa9-a8e5-eb3456e27073")]
    public sealed class Label : StringValueObject<Label>
    {
        private Label(string value) : base(value)
        {
            if(value.IsEmpty())
                throw new EmptyStringException(typeof(Label));

            Value = value;
        }

        [JsonConstructor]
        private Label(string value, bool deserialization = true) : base(value)
        {
            Value = value;
        }
    }
}