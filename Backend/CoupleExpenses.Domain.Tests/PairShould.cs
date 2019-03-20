using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Domain.Tests
{
    public sealed class PairShould
    {
        [Fact]        
        public void ReturnPairNameForAurelien()
        {
            Pair.Aurelien.ToString().Should().Be("Aurélien");
        }

        [Fact]
        public void ReturnPairNameForMarie() 
        {
            Pair.Marie.ToString().Should().Be("Marie");
        }
    }
}