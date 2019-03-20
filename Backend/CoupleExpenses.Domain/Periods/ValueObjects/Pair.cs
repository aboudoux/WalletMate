using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events.Structures;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("04c05bc7-2cb4-4964-953a-35156fbce95c")]
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