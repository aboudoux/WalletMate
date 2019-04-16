using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("SpendingCategory")]
    public class SpendingCategory : PositiveNumberValueObject<SpendingCategory>
    {
        public static SpendingCategory Common => new SpendingCategory(1);
        public static SpendingCategory Advance => new SpendingCategory(2);

        private SpendingCategory(int value) : base(value)
        {            
        }

        [JsonConstructor]
        private SpendingCategory(int value, bool _ = true) : base(value, _) {
        }

        public override string ToString()
        {
            return Value == 1
                ? "Commun"
                : "Avance";
        }
    }
}