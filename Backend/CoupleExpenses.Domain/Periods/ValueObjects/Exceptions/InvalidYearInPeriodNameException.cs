using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Periods.ValueObjects.Exceptions
{
    public class InvalidYearInPeriodNameException : CoupleExpensesException 
    {
        public InvalidYearInPeriodNameException(int year)
            : base($"The year {year} is invalid")
        {
        }
    }
}