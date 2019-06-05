using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Application.Pairs;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Common;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure
{
    public class InMemoryDatabaseRepository : IDatabaseRepository
    {
        private readonly IUserProvider _userProvider;
        private readonly Dictionary<PeriodName, IPeriodBalance> _allPeriods = new Dictionary<PeriodName, IPeriodBalance>();
        private readonly Dictionary<string, List<IPeriodOperation>> _operations = new Dictionary<string, List<IPeriodOperation>>();

        public InMemoryDatabaseRepository(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
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
                operation.Pair = pair.GetUserName(_userProvider);
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
            _allPeriods[periodId.ToPeriodName()] = new PeriodBalance(amountDue.Value, by.GetUserName(_userProvider));
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

        private static CultureInfo frCulture = new CultureInfo("fr-FR");

        public Task<IReadOnlyList<IPeriodOperation>> SearchOperation(string filter)
        {
            if (filter.IsEmpty())
                return Task.FromResult((IReadOnlyList<IPeriodOperation>)new List<IPeriodOperation>());

            var filterElements = filter.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            return Task.FromResult((IReadOnlyList<IPeriodOperation>)filterElements
                    .Aggregate(_operations.SelectMany(a=>a.Value).ToList(), Filter));

            List<IPeriodOperation> Filter(IReadOnlyList<IPeriodOperation> operations, string filterCriteria)
            {
                return operations
                    .Where(operation => operation.Label.Match(filterCriteria) ||
                                        operation.Amount.ToString(frCulture).Match(filterCriteria) ||
                                        operation.Pair.Match(filterCriteria) ||
                                        PeriodId.From(operation.PeriodId).ToPeriodName().ToString().Split(' ')[0].Match(filterCriteria) ||
                                        operation.Category.Match(filterCriteria) ||
                                        operation.Type.Match(filterCriteria))
                    .ToList();
            }

            double GetNumber(string source)
                => double.TryParse(source, out var result)
                    ? result
                    : -1;
        }
    }

    internal static class StringExtensions {
        public static bool Match(this string source, string dest)
            => source.ToLower().Contains(dest.ToLower());
    }
}