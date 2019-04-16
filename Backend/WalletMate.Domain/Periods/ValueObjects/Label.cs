using Newtonsoft.Json;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;
using WalletMate.Domain.Common.ValueObjects.Exceptions;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("Label")]
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