using CoupleExpenses.Domain.Common.ValueObjects;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public class OperationId : PositiveNumberValueObject<OperationId>
    {
        private OperationId(int value) : base(value)
        {
        }
    }
}