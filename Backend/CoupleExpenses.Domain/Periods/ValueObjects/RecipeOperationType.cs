using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Events;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class RecipeOperationType : PositiveNumberValueObject<RecipeOperationType> {
        public static RecipeOperationType PartiallyDue => new RecipeOperationType((int) RecipeOperationTypeInfo.PartiallyDue);
        public static RecipeOperationType TotallyDue => new RecipeOperationType((int) RecipeOperationTypeInfo.TotallyDue);

        private RecipeOperationType(int value) : base(value) {
        }
    }
}