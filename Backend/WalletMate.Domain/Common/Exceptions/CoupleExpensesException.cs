using System;
using System.Runtime.Serialization;

namespace WalletMate.Domain.Common.Exceptions
{
    public abstract class CoupleExpensesException : Exception {
        protected CoupleExpensesException() {
        }

        protected CoupleExpensesException(string message) : base(message) {
        }

        protected CoupleExpensesException(string message, Exception innerException) : base(message, innerException) {
        }

        protected CoupleExpensesException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}