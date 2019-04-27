using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Services;

namespace WalletMate.Infrastructure
{
    public class InMemoryDatabaseRepository : IDatabaseRepository
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly Dictionary<PeriodName, IPeriodBalance> _allPeriods = new Dictionary<PeriodName, IPeriodBalance>();
        private readonly Dictionary<string, List<IPeriodOperation>> _operations = new Dictionary<string, List<IPeriodOperation>>();

        public InMemoryDatabaseRepository(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
        }

        public void AddPeriod(PeriodName periodName)
        {
            if(!_allPeriods.ContainsKey(periodName))
                _allPeriods.Add(periodName, new PeriodBalance(0,""));
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

        public void UpdateOperation(PeriodId periodId, OperationId operationId, Amount amount = null, Label label = null, Pair pair = null, RecipeCategory recipeCategory = null, SpendingCategory spendingCategory = null)
        {
            if (!_operations.ContainsKey(periodId.Value))
                return;

            var operation = _operations[periodId.Value].FirstOrDefault(a => a.OperationId == operationId.Value);
            if(operation == null)
                return;

            if (amount != null)
                operation.Amount = amount.Value;
            if (label != null)
                operation.Label = label.Value;
            if (pair != null) {
                operation.Pair = pair.GetUserName(_configurationProvider);
                operation.PairValue = pair.Value;
            }
            if (recipeCategory != null) {
                operation.Category = recipeCategory.ToString();
                operation.CategoryValue = recipeCategory.Value;
            }
            if (spendingCategory != null) {
                operation.Category = spendingCategory.ToString();
                operation.CategoryValue = spendingCategory.Value;
            }
        }

        public Task<IPeriodBalance> GetBalance(PeriodId requestPeriodId)
        {
            return Task.FromResult(_allPeriods[requestPeriodId.ToPeriodName()]);
        }

        public void UpdateBalance(PeriodId periodId, Amount amountDue, Pair @by)
        {
            _allPeriods[periodId.ToPeriodName()] = new PeriodBalance(amountDue.Value, by.GetUserName(_configurationProvider));
        }

        public Task<IReadOnlyList<IPeriodResult>> GetAllPeriod() 
            => Task.FromResult((IReadOnlyList<IPeriodResult>)_allPeriods.Keys                
                .Select(p => new PeriodResult(p.ToString(), p.ToPeriodId().Value))
                .OrderByDescending(p=>p.PeriodId)
                .ToList());

        public Task<IReadOnlyList<IPeriodOperation>> GetAllOperation(PeriodId periodId)
        {
            IReadOnlyList<IPeriodOperation> result = _operations.ContainsKey(periodId.Value) 
                ? _operations[periodId.Value] 
                : new List<IPeriodOperation>();

            return Task.FromResult(result);
        }
    }
}