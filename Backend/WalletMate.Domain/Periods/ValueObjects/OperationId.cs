using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("OperationId")]
    public class OperationId : PositiveNumberValueObject<OperationId>
    {
        private OperationId(int value) : base(value)
        {
        }

        [JsonConstructor]
        private OperationId(int value, bool deserialization = true) : base(value, deserialization) {
        }
    }
}