using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("Pair")]
    public sealed class Pair : PositiveNumberValueObject<Pair>
    {
        public static Pair Aurelien => new Pair(1);
        public static Pair Marie => new Pair(2);

        private Pair(int value) : base(value)
        {
        }

        [JsonConstructor]
        private Pair(int value, bool deserialization = true) : base(value, deserialization) {
        }

        public override string ToString()
            => Value == 1 
                ? "Aurélien" 
                : "Marie";
    }
}