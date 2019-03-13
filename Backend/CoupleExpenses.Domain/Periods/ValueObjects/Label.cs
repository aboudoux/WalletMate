using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Exceptions;
using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public sealed class Label : StringValueObject<Label>
    {
        private Label(string value) : base(value)
        {
            if(value.IsEmpty())
                throw new EmptyStringException(typeof(Label));

            Value = value;
        }        
    }
}