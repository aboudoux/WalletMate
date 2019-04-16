using System;
using FluentAssertions;
using WalletMate.Domain.Periods.Exceptions;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Domain.Tests
{
    public sealed class PeriodIdShould
    {
        [Fact]
        public void Be_created_from_month_and_year()
        {
            var periodId = PeriodId.From(1, 2018);
            periodId.Value.Should().Be("2018-01");
        }

        [Fact]
        public void be_created_from_periodName()
        {
            var periodName = PeriodName.From(1, 2018);
            var periodId = PeriodId.From(periodName);
            periodId.Value.Should().Be("2018-01");
        }

        [Fact]
        public void be_equatable()
        {
            var id1 = PeriodId.From(2, 2019);
            var id2 = PeriodId.From(PeriodName.From(2, 2019));

            id1.Should().Be(id2);
        }

        [Theory]
        [InlineData("2018-01")]
        [InlineData("1980-05")]
        [InlineData("2019-12")]
        [InlineData("1990-11")]
        [InlineData("2100-12")]
        public void Dont_throw_error_for_good_string_value(string id)
        {
            var periodId = PeriodId.From(id);
            periodId.Value.Should().Be(id);
        }

        [Theory]
        [InlineData("1979-01")]
        [InlineData("1980-13")]
        [InlineData("201912")]
        [InlineData("1945490-11")]
        [InlineData("2100-15454542")]
        [InlineData("AAAAAA")]
        [InlineData("1234-56")]
        public void Throw_Error_If_bad_format(string id)
        {
            Action action = () => PeriodId.From(id);
            action.Should().Throw<BadPeriodIdException>();
        }
    }
}