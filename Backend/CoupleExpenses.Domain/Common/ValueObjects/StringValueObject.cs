namespace CoupleExpenses.Domain.Common.ValueObjects
{
    public abstract class StringValueObject<T> : ValueObject<string>
        where T : class {
        protected StringValueObject(string value)
            => Value = value;

        public static T From(string value)
            => CreatePrivateInstance<T>(value);

        public static implicit operator string(StringValueObject<T> source)
            => source.Value;
    }
}