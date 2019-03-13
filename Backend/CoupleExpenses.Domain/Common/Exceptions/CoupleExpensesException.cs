using System;
using System.Runtime.Serialization;

namespace CoupleExpenses.Domain.Common.Exceptions
{
    public class CoupleExpensesException : Exception {
        public CoupleExpensesException() {
        }

        public CoupleExpensesException(string message) : base(message) {
        }

        public CoupleExpensesException(string message, Exception innerException) : base(message, innerException) {
        }

        protected CoupleExpensesException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}