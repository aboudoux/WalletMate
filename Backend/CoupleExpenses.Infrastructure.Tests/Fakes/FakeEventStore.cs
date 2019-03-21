using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Infrastructure.Tests.Fakes 
{
    public class FakeEventStore : IEventStore
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        public Task<IDomainEvent[]> GetEvents(Predicate<IDomainEvent> predicate)
        {
            return Task.FromResult(_events.Where(e => predicate(e)).Select(a => a).ToArray());
        }

        public Task Save(IDomainEvent @event)
        {
            _events.Add(@event);
            return Task.CompletedTask;
        }

        public Task<int> GetLastSequence(string aggregateId)
        {
            var selection = _events.Where(a => a.AggregateId == aggregateId).ToList();
            return Task.FromResult(selection.Any() ? _events.Max(a => a.Sequence) : -1);
        }
    }
}
