using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Common.ValueObjects
{
    public abstract class PositiveNumberValueObject<T> : ValueObject<int>
        where T : class    
    {
        protected PositiveNumberValueObject(int value)
        {
            if(value <= 0)
                throw new NegativeNumberException(typeof(T));
            Value = value;
        }

        protected PositiveNumberValueObject(int value, bool _)
        {
            Value = value;
        }

        public static T From(int value)
            => CreatePrivateInstance<T>(value);
    }
}