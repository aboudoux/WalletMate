using System;
using System.Runtime.Serialization;

namespace WalletMate.Domain.Common.Exceptions
{
    [Serializable]
    public class NoPlayersFoundException : CoupleExpensesException {
        public NoPlayersFoundException() {
        }

        protected NoPlayersFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}