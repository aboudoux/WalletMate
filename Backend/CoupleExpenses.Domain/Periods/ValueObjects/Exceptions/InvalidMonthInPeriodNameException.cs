using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Periods.ValueObjects.Exceptions {
    public class InvalidMonthInPeriodNameException : CoupleExpensesException
    {
        public InvalidMonthInPeriodNameException(int month) 
            : base($"The month number {month} is invalid")
        {
        }
    }
}
