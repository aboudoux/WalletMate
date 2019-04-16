using System;
using System.Runtime.Serialization;
using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Common.ValueObjects.Exceptions
{
    [Serializable]
    public class EmptyStringException : WalletMateException {
        public EmptyStringException(Type valueObjectType)
            : base($"A '{valueObjectType.Name}' cannot be an empty string.") {
        }

        protected EmptyStringException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}