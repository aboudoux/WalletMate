using System;
using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("Pair")]
    public sealed class Pair : PositiveNumberValueObject<Pair>
    {
        public static Pair Aurelien => new Pair(1);
        public static Pair Marie => new Pair(2);

        private Pair(int value) : base(value)
        {
            if(value < 1 || value > 2)
                throw new Exception("invalid pair");
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