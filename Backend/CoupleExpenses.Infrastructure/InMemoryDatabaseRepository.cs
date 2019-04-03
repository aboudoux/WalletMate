using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Dto;

namespace CoupleExpenses.Infrastructure
{
    public class InMemoryDatabaseRepository : IDatabaseRepository
    {
        private readonly HashSet<string> _allPeriods = new HashSet<string>();
        private readonly Dictionary<string, List<IPeriodOperation>> _operations = new Dictionary<string, List<IPeriodOperation>>();

        public void AddPeriod(PeriodName periodName)
        {
            _allPeriods.Add(periodName.ToString());
        }

        public void AddOperation(IPeriodOperation operation)
        {
            if(!_operations.ContainsKey(operation.PeriodId))
                _operations.Add(operation.PeriodId, new List<IPeriodOperation>());

            _operations[operation.PeriodId].Add(operation);
        }

        public Task<IReadOnlyList<string>> GetAllPeriod() 
            => Task.FromResult((IReadOnlyList<string>)_allPeriods.ToList());

        public Task<IReadOnlyList<IPeriodOperation>> GetAllOperation(PeriodId periodId)
        {
            IReadOnlyList<IPeriodOperation> result = _operations.ContainsKey(periodId.Value) 
                ? _operations[periodId.Value] 
                : new List<IPeriodOperation>();

            return Task.FromResult(result);
        }
    }
}