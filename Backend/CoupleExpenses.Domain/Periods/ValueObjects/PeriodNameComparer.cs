using System;
using System.Collections.Generic;

namespace WalletMate.Domain.Periods.ValueObjects
{
    public sealed  class PeriodNameComparer : IComparer<PeriodName>
    {
        public int Compare(PeriodName x, PeriodName y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (x == y) return 0;
            if (x.Year > y.Year)
                return 1;
            if (x.Year < y.Year)
                return -1;
            if(x.Month>y.Month)
                return 1;
            if (x.Month < y.Month)
                return -1;

            return 0;
        }
    }
}