using System.Collections.Generic;
using WalletMate.Application.Core;

namespace WalletMate.Application.Periods.Queries
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