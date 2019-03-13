using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;
using CoupleExpenses.Domain.Periods.Events;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class SpendingOperationType : PositiveNumberValueObject<SpendingOperationType>
    {
        public static SpendingOperationType Common => new SpendingOperationType((int)SpendingOperationTypeInfo.Common);
        public static SpendingOperationType Advance => new SpendingOperationType((int)SpendingOperationTypeInfo.Advance);

        private SpendingOperationType(int value) : base(value)
        {            
        }
    }
}