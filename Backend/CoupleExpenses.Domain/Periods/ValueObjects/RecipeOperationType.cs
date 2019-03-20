using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events.Structures;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("004aac27-3b35-4a62-9645-6e797cef3cd0")]
    public class RecipeOperationType : PositiveNumberValueObject<RecipeOperationType> 
    {
        public static RecipeOperationType Common => new RecipeOperationType(1);
        public static RecipeOperationType Individual => new RecipeOperationType(2);

        private RecipeOperationType(int value) : base(value) {
        }

        [JsonConstructor]
        private RecipeOperationType(int value, bool _ = true) : base(value, _) {
        }
    }
}