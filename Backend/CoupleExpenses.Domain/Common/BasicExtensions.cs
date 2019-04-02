using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action) 
        {
            if (source == default) 
                return;            

            foreach (var element in source) 
                await action(element);            
        }

        public static string ToBase64(this string source)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
