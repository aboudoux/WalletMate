using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events.Structures;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("SpendingOperationType")]
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