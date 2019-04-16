using System;
using System.Runtime.Serialization;

namespace WalletMate.Domain.Common.Exceptions
{
    [Serializable]
    public class AggregateNotFoundException : CoupleExpensesException {
        public AggregateNotFoundException(Type aggregateType)
            : base($"Aggregate {aggregateType.Name} not found.") {
        }

        protected AggregateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}