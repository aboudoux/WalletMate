using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.Events.Structures;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class RecipeOperationType : PositiveNumberValueObject<RecipeOperationType> {
        public static RecipeOperationType Common => new RecipeOperationType((int) RecipeOperationTypeInfo.Common);
        public static RecipeOperationType Individual => new RecipeOperationType((int) RecipeOperationTypeInfo.Individual);

        private RecipeOperationType(int value) : base(value) {
        }
    }
}