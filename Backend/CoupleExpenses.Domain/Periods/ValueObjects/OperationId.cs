using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("2fe896e5-89fe-44db-82df-700f5c6d8fa2")]
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