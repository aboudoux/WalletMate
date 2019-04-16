using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Periods.ValueObjects.Exceptions {
    public class InvalidMonthInPeriodNameException : CoupleExpensesException
    {
        public InvalidMonthInPeriodNameException(int month) 
            : base($"The month number {month} is invalid")
        {
        }
    }
}
