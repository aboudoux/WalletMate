using System.Collections.Generic;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Core
{
    public interface IDatabaseRepository
    {
        void AddPeriod(PeriodName periodName);
        Task<IReadOnlyList<string>> GetAllPeriod();
    }
}