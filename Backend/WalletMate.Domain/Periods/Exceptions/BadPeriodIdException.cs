using WalletMate.Domain.Common.Exceptions;

namespace WalletMate.Domain.Periods.Exceptions
{
    public sealed class BadPeriodIdException : WalletMateException
    {
        public BadPeriodIdException(string value)
            : base($"L'identifiant de periode {value} est incorrect")
        {            
        }
    }
}