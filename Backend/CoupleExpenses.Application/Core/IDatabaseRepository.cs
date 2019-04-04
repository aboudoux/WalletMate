using System.Collections.Generic;
using System.Threading.Tasks;
using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Periods.ValueObjects;

namespace CoupleExpenses.Application.Core
{
    public interface IDatabaseRepository
    {
        void AddPeriod(PeriodName periodName);
        Task<IReadOnlyList<string>> GetAllPeriod();
        Task<IReadOnlyList<IPeriodOperation>> GetAllOperation(PeriodId periodId);
        void AddOperation(IPeriodOperation operation);
        void RemoveOperation(PeriodId periodId, OperationId operationId);
        Task<IPeriodBalance> GetBalance(PeriodId requestPeriodId);
        void UpdateBalance(PeriodId periodId, Amount amountDue, Pair by);
    }
}