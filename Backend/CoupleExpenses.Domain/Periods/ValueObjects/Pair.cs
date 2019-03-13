using CoupleExpenses.Domain.Common.ValueObjects;
using CoupleExpenses.Domain.Common.ValueObjects.Exceptions;
using CoupleExpenses.Domain.Periods.Events;

namespace CoupleExpenses.Domain.Periods.ValueObjects
{
    public sealed class Pair : PositiveNumberValueObject<Pair>
    {
        public static Pair Aurelien => new Pair((int)PairInfo.Aurelien);
        public static Pair Marie => new Pair((int)PairInfo.Marie);

        private Pair(int value) : base(value)
        {
        }

        public override string ToString() 
            => ((PairInfo) Value).ToString();
    }
}