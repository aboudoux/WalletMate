using System;
using System.Runtime.Serialization;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common.ValueObjects.Exceptions
{
    [Serializable]
    public class EmptyStringException : CoupleExpensesException {
        public EmptyStringException(Type valueObjectType)
            : base($"A '{valueObjectType.Name}' cannot be an empty string.") {
        }

        protected EmptyStringException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}