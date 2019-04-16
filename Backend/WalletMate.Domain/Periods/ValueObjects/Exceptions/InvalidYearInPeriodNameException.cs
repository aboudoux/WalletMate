using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Periods.ValueObjects.Exceptions
{
    public class InvalidYearInPeriodNameException : WalletMateException 
    {
        public InvalidYearInPeriodNameException(int year)
            : base($"The year {year} is invalid")
        {
        }
    }
}