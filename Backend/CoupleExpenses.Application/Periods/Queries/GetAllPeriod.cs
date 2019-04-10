using System.Collections.Generic;
using CoupleExpenses.Application.Core;

namespace CoupleExpenses.Application.Periods.Queries
{
    public class GetAllPeriod : IQuery<IReadOnlyList<IPeriodResult>>
    {        
    }

    public interface IPeriodResult
    {
        string PeriodName { get; }
        string PeriodId { get; }
    }
}