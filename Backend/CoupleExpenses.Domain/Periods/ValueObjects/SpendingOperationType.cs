using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
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

        public override string ToString()
        {
            return Value == 1
                ? "Commun"
                : "Avance";
        }
    }
}