using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Infrastructure
{
    public class InMemoryDatabaseRepository : IDatabaseRepository
    {
        private readonly HashSet<string> _allPeriods = new HashSet<string>();

        public void AddPeriod(PeriodName periodName)
        {
            _allPeriods.Add(periodName.ToString());
        }

        public Task<IReadOnlyList<string>> GetAllPeriod() 
            => Task.FromResult((IReadOnlyList<string>)_allPeriods.ToList());
    }
}