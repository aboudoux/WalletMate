using System;
using System.Collections.Generic;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.ValueObjects.Exceptions;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class PeriodName : ValueObject<int, int>
    {
        public int Month => Value1;
        public int Year => Value2;

        private PeriodName(int month, int year)
        {
            if (month < 1 || month > 12)
                throw new InvalidMonthInPeriodNameException(month);

            if( year < 2000 || year > 9999)
                throw new InvalidYearInPeriodNameException(year);

            Value1 = month;
            Value2 = year;
        }

        public static PeriodName From(int month, int year) 
            => CreatePrivateInstance<PeriodName>(month, year);

        private readonly Dictionary<int, string> _months = new Dictionary<int, string>()
        {
            {1, "Janvier"},
            {2, "Février"},
            {3, "Mars"},
            {4, "Avril"},
            {5, "Mai"},
            {6, "Juin"},
            {7, "Juillet"},
            {8, "Aout"},
            {9, "Septembre"},
            {10, "Octobre"},
            {11, "Novembre"},
            {12, "Décembre"},
        };

        public override string ToString()
        {
            return $"{_months[Month]} {Year}";
        }

        public PeriodName GetIncrement()
        {
            return From(
                IsDecember() ? 1 : Month+1,
                IsDecember() ? Year+1 : Year);

            bool IsDecember() => Month == 12;
        }
    }
}