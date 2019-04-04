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
        private readonly Dictionary<string,IPeriodBalance> _allPeriods = new Dictionary<string, IPeriodBalance>();
        private readonly Dictionary<string, List<IPeriodOperation>> _operations = new Dictionary<string, List<IPeriodOperation>>();

        public void AddPeriod(PeriodName periodName)
        {
            if(!_allPeriods.ContainsKey(periodName.ToString()))
                _allPeriods.Add(periodName.ToString(), new PeriodBalance(0,""));
        }

        public void AddOperation(IPeriodOperation operation)
        {
            if(!_operations.ContainsKey(operation.PeriodId))
                _operations.Add(operation.PeriodId, new List<IPeriodOperation>());

            _operations[operation.PeriodId].Add(operation);
        }

        public void RemoveOperation(PeriodId periodId, OperationId operationId)
        {
            if (_operations.ContainsKey(periodId.Value))
                _operations[periodId.Value].RemoveAll(o => o.OperationId == operationId.Value);
        }

        public Task<IPeriodBalance> GetBalance(PeriodId requestPeriodId)
        {
            return Task.FromResult(_allPeriods[requestPeriodId.Value]);
        }

        public void UpdateBalance(PeriodId periodId, Amount amountDue, Pair @by)
        {
            _allPeriods[periodId.Value] = new PeriodBalance(amountDue.Value, by.ToString());
        }

        public Task<IReadOnlyList<string>> GetAllPeriod() 
            => Task.FromResult((IReadOnlyList<string>)_allPeriods.Keys.ToList());

        public Task<IReadOnlyList<IPeriodOperation>> GetAllOperation(PeriodId periodId)
        {
            IReadOnlyList<IPeriodOperation> result = _operations.ContainsKey(periodId.Value) 
                ? _operations[periodId.Value] 
                : new List<IPeriodOperation>();

            return Task.FromResult(result);
        }
    }
}