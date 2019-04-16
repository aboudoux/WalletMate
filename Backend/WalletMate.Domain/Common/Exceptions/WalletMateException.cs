using System;
using System.Runtime.Serialization;

namespace WalletMate.Domain.Common.Exceptions
{
    public abstract class WalletMateException : Exception {
        protected WalletMateException() {
        }

        protected WalletMateException(string message) : base(message) {
        }

        protected WalletMateException(string message, Exception innerException) : base(message, innerException) {
        }

        protected WalletMateException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}