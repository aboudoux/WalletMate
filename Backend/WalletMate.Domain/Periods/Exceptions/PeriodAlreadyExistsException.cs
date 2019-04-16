using WalletMate.Domain.Common.Exceptions;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.Domain.Periods.Exceptions {
    public sealed class PeriodAlreadyExistsException : WalletMateException
    {
        public PeriodAlreadyExistsException(PeriodName periodName) 
            : base($"La période {periodName} existe déjà.")
        {
        }
    }
}
