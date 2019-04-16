using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Periods.ValueObjects.Exceptions {
    public class InvalidMonthInPeriodNameException : WalletMateException
    {
        public InvalidMonthInPeriodNameException(int month) 
            : base($"The month number {month} is invalid")
        {
        }
    }
}
