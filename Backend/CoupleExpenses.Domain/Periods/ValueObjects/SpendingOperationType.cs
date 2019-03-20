using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events.Structures;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("76d99fb5-5bf6-4457-80db-60562c460c7a")]
    public class SpendingOperationType : PositiveNumberValueObject<SpendingOperationType>
    {
        public static SpendingOperationType Common => new SpendingOperationType(1);
        public static SpendingOperationType Advance => new SpendingOperationType(2);

        private SpendingOperationType(int value) : base(value)
        {            
        }

        [JsonConstructor]
        private SpendingOperationType(int value, bool _ = true) : base(value, _) {
        }
    }
}