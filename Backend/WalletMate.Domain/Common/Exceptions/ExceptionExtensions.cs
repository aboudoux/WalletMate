using System;

namespace WalletMate.Domain.Common.Exceptions
{
    public static class ExceptionExtensions {
        public static Exception LastException(this Exception source) {
            var currentException = source;
            while (currentException.InnerException != null)
                currentException = currentException.InnerException;
            return currentException;
        }
    }
}