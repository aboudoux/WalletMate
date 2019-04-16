using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;

namespace WalletMate.Domain.Periods.ValueObjects
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