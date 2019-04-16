namespace WalletMate.Domain.Common.ValueObjects
{
    public abstract class EmptyStringValueObject<T> : StringValueObject<T>
        where T : class {
        public static T Empty => CreatePrivateInstance<T>(string.Empty);

        protected EmptyStringValueObject(string value) : base(value) {
        }
    }
}