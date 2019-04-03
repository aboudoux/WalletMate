using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Periods.Exceptions
{
    public sealed class BadPeriodIdException : CoupleExpensesException
    {
        public BadPeriodIdException(string value)
            : base($"L'identifiant de periode {value} est incorrect")
        {            
        }
    }
}