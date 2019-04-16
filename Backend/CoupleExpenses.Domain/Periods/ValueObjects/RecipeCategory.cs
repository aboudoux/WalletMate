using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("RecipeCategory")]
    public class RecipeCategory : PositiveNumberValueObject<RecipeCategory> 
    {
        public static RecipeCategory Common => new RecipeCategory(1);
        public static RecipeCategory Individual => new RecipeCategory(2);

        private RecipeCategory(int value) : base(value) {
        }

        [JsonConstructor]
        private RecipeCategory(int value, bool _ = true) : base(value, _) {
        }

        public override string ToString()
        {
            return Value == 1
                ? "Commun"
                : "Individuelle";
        }
    }
}