using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
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