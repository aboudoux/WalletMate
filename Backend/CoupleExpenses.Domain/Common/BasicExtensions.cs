namespace CoupleExpenses.Domain.Common 
{
    public static class BasicExtensions
    {
        public static bool IsEmpty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}
