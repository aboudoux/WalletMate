using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects
{
    public abstract class PositiveDoubleValueObject<T> : ValueObject<double>
        where T : class 
    {
        protected PositiveDoubleValueObject(double value) {
            if (value <= 0)
                throw new NegativeNumberException(typeof(T));
            Value = value;
        }

        public static T From(double value)
            => CreatePrivateInstance<T>(value);
    }
}