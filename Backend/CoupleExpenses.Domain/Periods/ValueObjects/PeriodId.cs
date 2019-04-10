using System;
using System.Text.RegularExpressions;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Periods.Exceptions;
using Newtonsoft.Json;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    [SerializableTypeIdentifier("PeriodId")]
    public class PeriodId : ValueObject<string>
    {        
        private PeriodId(int month, int year)
        {
            Value = $"{year:D4}-{month:D2}";
        }

        private PeriodId(string id)
        {
            if (id.IsEmpty())
                throw new BadPeriodIdException(id);

            var splittedId = id.Split('-');
            if(splittedId.Length != 2)
                throw new BadPeriodIdException(id);

            if(!int.TryParse(splittedId[0], out int year) ||
            !int.TryParse(splittedId[1], out int month) )
                throw new BadPeriodIdException(id);

            if (year < 1980 || year > 3000)
                throw new BadPeriodIdException(id);
            if(month < 1 || month > 12)
                throw new BadPeriodIdException(id);

            Value = id;
        }

        [JsonConstructor]
        private PeriodId(string value, bool _ = true)
        {
            Value = value;
        }

        public static PeriodId From(int month, int year) 
            => new PeriodId(month, year);

        public static PeriodId From(PeriodName periodName)
            => new PeriodId(periodName.Month, periodName.Year);

        public static PeriodId From(string id)
            => new PeriodId(id);

        public PeriodName ToPeriodName()
        {
            var splittedValue = Value.Split('-');
            var year = int.Parse(splittedValue[0]);
            var month= int.Parse(splittedValue[1]);
            return PeriodName.From(month, year);
        }
    }
}