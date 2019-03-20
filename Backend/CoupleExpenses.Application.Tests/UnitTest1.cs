using CoupleExpenses.Application.Core;
using CoupleExpenses.Application.Periods;
using CoupleExpenses.Domain.Periods.ValueObjects;
using Xunit;

namespace CoupleExpenses.Application.Tests
{
    public class PeriodCommandHandlerShould
    {
        [Fact]
        public void Be_called_when_posting_CreatePeriod()
        {
            ICommandBus bus = new MediatorCommandBus();
            bus.SendAsync(new CreatePeriod(PeriodName.From(1, 2000)));
        }
    }
}
