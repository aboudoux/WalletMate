using System;
using System.Collections.Generic;

namespace CoupleExpenses.Domain.Common 
{
    public static class BasicExtensions
    {
        public static bool IsEmpty(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action(element);
            }
        }
    }
}
