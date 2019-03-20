using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Infrastructure.Tests
{
    public class FileEventStoreShould
    {
        [Fact]
        public async Task Save_PeriodCreated_in_csv_file()
        {
            var store = new FileEventStore(new CustomJsonSerializer());
            await store.Save(new PeriodCreated(PeriodName.From(1, 2017)));

            var @event = await store.GetEvents(a => true);
            @event.First().Should().BeOfType<PeriodCreated>().Which.PeriodName.Should().Be(PeriodName.From(1, 2017));
        }

        [Fact]
        public async Task Save_SpendingAdded_in_csv_file() {
            var store = new FileEventStore(new CustomJsonSerializer());
            await store.Save(new SpendingAdded(OperationId.From(5)));

            var @event = await store.GetEvents(a => true);
            @event.First().Should().BeOfType<PeriodCreated>().Which.PeriodName.Should().Be(PeriodName.From(1, 2017));
        }
    }
}
