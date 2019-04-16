using System;
using FluentAssertions;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.Exceptions;
using WalletMate.Domain.Periods.ValueObjects;
using Xunit;

namespace WalletMate.Domain.Tests
{
    public sealed class PeriodCreatorShould
    {
        [Fact]
        public void Create_new_period()
        {
            var periodCreator = new PeriodCreator(History.Empty);
            var period = periodCreator.Create(PeriodName.From(3, 2019));

            period.PeriodName.Should().Be(PeriodName.From(3, 2019));
        }

        [Fact]
        public void Throw_error_if_create_period_that_already_exists()
        {
            var periodCreator = Some.PeriodCreator(e => 
                e.WithEvent(Some.PeriodCreated(PeriodName.From(3, 2019))));

            Action action = () => periodCreator.Create(PeriodName.From(3, 2019));
            action.Should().Throw<PeriodAlreadyExistsException>();
        }

        [Fact]
        public void Throw_error_if_create_same_period_twice()
        {
            var periodCreator = Some.PeriodCreator();
            periodCreator.Create(PeriodName.From(3, 2019));
            Action action = () => periodCreator.Create(PeriodName.From(3, 2019));
            action.Should().Throw<PeriodAlreadyExistsException>();
        }

        [Fact]
        public void create_next_period()
        {
            var periodCreator = Some.PeriodCreator(e => 
                e.WithEvent(Some.PeriodCreated(PeriodName.From(1, 2000)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(12, 2010)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(5, 2005)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(8, 2010)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(9, 2010)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(10, 2010)))
                 .WithEvent(Some.PeriodCreated(PeriodName.From(11, 2010)))                
                );

            var newPeriod = periodCreator.CreateNext();
            newPeriod.PeriodName.Should().Be(PeriodName.From(1, 2011));
            newPeriod.UncommittedEvents.GetStream().Should().HaveCount(1);
        }
    }
}