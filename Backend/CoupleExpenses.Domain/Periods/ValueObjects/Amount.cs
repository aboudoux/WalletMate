using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class Amount : PositiveDoubleValueObject<Amount>
    {
        private Amount(double value) : base(value)
        {
        }
    }
}