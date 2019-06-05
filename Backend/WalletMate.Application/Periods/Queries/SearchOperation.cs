using System;
using System.Collections.Generic;
using WalletMate.Application.Core;

namespace WalletMate.Application.Periods.Queries
{
    public class SearchOperation : IQuery<IReadOnlyList<IPeriodOperation>>
    {
        public SearchOperation(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; }
    }
}