using CoupleExpenses.Domain.Common.Exceptions;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Domain.Periods.Exceptions {
    public sealed class PeriodAlreadyExistsException : CoupleExpensesException
    {
        public PeriodAlreadyExistsException(PeriodName periodName) 
            : base($"La période {periodName} existe déjà.")
        {
        }
    }
}
