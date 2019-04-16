using FluentAssertions;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Domain.Tests
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