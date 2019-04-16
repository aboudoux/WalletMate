using System.Collections.Generic;
using Newtonsoft.Json;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.ValueObjects;
using WalletMate.Domain.Periods.ValueObjects.Exceptions;

namespace WalletMate.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("PeriodName")]
    public class PeriodName : ValueObject<int, int>
    {        
        [JsonIgnore]
        public int Month => Value1;
        [JsonIgnore]
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

        [JsonConstructor]
        private PeriodName(int value1, int value2, bool _ = true)
        {
            Value1 = value1;
            Value2 = value2;
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

        public PeriodId ToPeriodId()
            => PeriodId.From(this);
    }
}