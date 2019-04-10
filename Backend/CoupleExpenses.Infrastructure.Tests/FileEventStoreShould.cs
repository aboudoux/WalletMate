using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Periods;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Tests.Tools;
using FluentAssertions;
using Xunit;

namespace CoupleExpenses.Infrastructure.Tests
{
    public class FileEventStoreShould
    {
        [Fact]
        public async Task Save_PeriodCreated_in_csv_file()
        {
            await Make.TestFile("period.es").AndExecute(async environment =>
            {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), environment.FilePath);
                
                await store.Save(new PeriodCreated(PeriodName.From(1, 2017)));

                var @event = await store.GetEvents(b => true);
                @event.Last().Should().BeOfType<PeriodCreated>().Which.PeriodName.Should().Be(PeriodName.From(1, 2017));                
            });            
        }

        [Fact]
        public async Task Save_SpendingAdded_in_csv_file()
        {
            await Make.TestFile("SpendingAdded.es").AndExecute(async env =>
            {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);
                await store.Save(new SpendingAdded(OperationId.From(5), Amount.From(15.2), Label.From("this is a test"), Pair.Aurelien, SpendingCategory.Advance));
                var @event = await store.GetEvents(a => true);
                @event.Last().Should().BeOfType<SpendingAdded>().Which.OperationId.Should().Be(OperationId.From(5));
            });
        }

        [Fact]
        public async Task Retrieve_last_sequence()
        {
            await Make.TestFile("Sequence.es").AndExecute(async env =>
            {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);
                
                var period = Period.Create(PeriodName.From(1, 2007));
                period.AddSpending(Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test2"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test3"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test4"), Pair.Aurelien, SpendingCategory.Common);

                await store.Save(period.UncommittedEvents.GetStream());

                var lastSequence = await store.GetLastSequence(period.AggregateId);
                lastSequence.Should().Be(8);                
            });
        }

        [Fact]
        public async Task Write_in_eventstore_from_multiple_thread()
        {
            await Make.TestFile("MultiThread.es").AndExecute(async env =>
            {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);
                
                var period = Period.Create(PeriodName.From(1, 2007));
                period.AddSpending(Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test2"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test3"), Pair.Aurelien, SpendingCategory.Common);
                period.AddSpending(Amount.From(5), Label.From("test4"), Pair.Aurelien, SpendingCategory.Common);

                var t1 = Task.Run(() => store.Save(period.UncommittedEvents.GetStream()));
                var t2 = Task.Run(() => store.Save(period.UncommittedEvents.GetStream()));

                Task.WaitAll(t1, t2);

                var lastSequence = await store.GetLastSequence(period.AggregateId);
                lastSequence.Should().Be(8);                
            });
        }

        [Fact]
        public async Task Return_sequence0_if_event_stream_is_empty()
        {
            await Make.TestFile("sequence0.es").AndExecute(async env => {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);

                var lastSequence = await store.GetLastSequence("test");
                lastSequence.Should().Be(-1);
            });
        }

        [Fact]
        public async Task Reload_all_events_with_their_domainEvent_infos()
        {
            await Make.TestFile("reload.es").AndExecute(async env =>
            {
                var store = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);

                var period = Period.Create(PeriodName.From(1, 2007));
                period.AddSpending(Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingCategory.Common);
                await store.Save(period.UncommittedEvents.GetStream());

                var store2 = new FileEventStoreWithCache(new CustomJsonSerializer(), env.FilePath);

                var allEvents = await store2.GetEvents(a=> true);
                allEvents.Any(a => a.AggregateId.IsEmpty()).Should().BeFalse();
            });
        }
    }
}
