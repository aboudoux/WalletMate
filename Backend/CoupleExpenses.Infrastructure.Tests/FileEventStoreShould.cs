using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Periods;
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
            await Make.TestFile("period.es").AndExecute(async environment =>
            {
                var store = new FileEventStoreWithCache(environment.FilePath, new CustomJsonSerializer());
                
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
                var store = new FileEventStoreWithCache(env.FilePath, new CustomJsonSerializer());
                await store.Save(new SpendingAdded(OperationId.From(5), Amount.From(15.2), Label.From("this is a test"), Pair.Aurelien, SpendingOperationType.Advance));
                var @event = await store.GetEvents(a => true);
                @event.Last().Should().BeOfType<SpendingAdded>().Which.OperationId.Should().Be(OperationId.From(5));
            });
        }

        [Fact]
        public async Task Retrieve_last_sequence()
        {
            await Make.TestFile("Sequence.es").AndExecute(async env =>
            {
                var store = new FileEventStoreWithCache(env.FilePath, new CustomJsonSerializer());
                
                var period = Period.Create(PeriodName.From(1, 2007));
                period.AddSpending(Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test2"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test3"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test4"), Pair.Aurelien, SpendingOperationType.Common);

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
                var store = new FileEventStoreWithCache(env.FilePath, new CustomJsonSerializer());
                
                var period = Period.Create(PeriodName.From(1, 2007));
                period.AddSpending(Amount.From(5), Label.From("test"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test2"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test3"), Pair.Aurelien, SpendingOperationType.Common);
                period.AddSpending(Amount.From(5), Label.From("test4"), Pair.Aurelien, SpendingOperationType.Common);

                var t1 = Task.Run(() => store.Save(period.UncommittedEvents.GetStream()));
                var t2 = Task.Run(() => store.Save(period.UncommittedEvents.GetStream()));

                Task.WaitAll(t1, t2);

                var lastSequence = await store.GetLastSequence(period.AggregateId);
                lastSequence.Should().Be(8);                
            });
        }
    }
}
