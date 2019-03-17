using System;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Domain.Periods.ValueObjects.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Domain.Tests
{
    public class PeriodNameShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(13)]
        [InlineData(20)]
        [InlineData(-100)]
        public void Throw_error_if_invalid_month(int month)
        {
            Action instantiation = () => PeriodName.From(month, 2007);
            instantiation.Should().Throw<InvalidMonthInPeriodNameException>().Which.Message.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(1999)]
        [InlineData(10000)]
        [InlineData(1980)]
        [InlineData(2135454)]
        [InlineData(-4)]
        public void Throw_error_if_invalid_year(int year)
        {
            Action instantiation = () => PeriodName.From(1, year);
            instantiation.Should().Throw<InvalidYearInPeriodNameException>().Which.Message.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(1, 2005)]
        [InlineData(2, 2010)]
        [InlineData(10, 2025)]
        [InlineData(5, 2030)]
        public void Be_equatable_to_another_period_name(int month, int year)
        {
            var period1 = PeriodName.From(month, year);
            var period2 = PeriodName.From(month, year);

            period1.Should().BeEquivalentTo(period2);
            period1.Should().Be(period2);
            (period1 == period2).Should().BeTrue();
        }

        [Theory]
        [InlineData(1, 2005)]
        [InlineData(2, 2010)]
        [InlineData(10, 2025)]
        [InlineData(5, 2030)]
        public void Not_be_equatable_for_different_period_name(int month, int year)
        {
            var period1 = PeriodName.From(month, year);
            var period2 = PeriodName.From(month+1, year+1);

            period1.Should().NotBe(period2);
            (period1 != period2).Should().BeTrue();
        }

        [Theory]
        [InlineData(1,2000, "Janvier 2000")]
        [InlineData(2,2001, "Février 2001")]
        [InlineData(3,2002, "Mars 2002")]
        [InlineData(4,2003, "Avril 2003")]
        [InlineData(5,2004, "Mai 2004")]
        [InlineData(6,2005, "Juin 2005")]
        [InlineData(7,2006, "Juillet 2006")]
        [InlineData(8,2007, "Aout 2007")]
        [InlineData(9,2008, "Septembre 2008")]
        [InlineData(10,2009, "Octobre 2009")]
        [InlineData(11,2010, "Novembre 2010")]
        [InlineData(12,2011, "Décembre 2011")]
        
        public void Return_period_name_as_string(int month, int year, string expectedName)
        {
            var periodName = PeriodName.From(month, year);
            periodName.ToString().Should().Be(expectedName);
        }

        [Theory]
        [InlineData(1, 2000, 2, 2000)]
        [InlineData(5, 2000, 6, 2000)]
        [InlineData(11, 2010, 12, 2010)]
        [InlineData(12, 2010, 1, 2011)]
        public void return_an_incremnte_of_itself(int prevMonth, int prevYear, int nextMonth, int nextYear)
        {
            var periodName = PeriodName.From(prevMonth, prevYear);
            var next = periodName.GetIncrement();
            next.Should().Be(PeriodName.From(nextMonth, nextYear));
        }
    }
}