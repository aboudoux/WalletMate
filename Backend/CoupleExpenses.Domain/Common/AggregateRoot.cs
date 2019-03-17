using System;
using System.Collections.Generic;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common {
    public abstract class AggregateRoot<TState> where TState : AggregateState, new()
    {
        protected readonly TState State = new TState();
        public string AggregateId { get; protected set; }
        private int _lastSequenceNumber = -1;

        public UncommittedEvents UncommittedEvents { get; } = new UncommittedEvents();

        protected AggregateRoot(History history)
        {
            HydrateFrom(history);
        }

        protected static T CreateNew<T>(string aggregateId, IDomainEvent creationEvent) 
            where T : AggregateRoot<TState>
        {
            var aggregate = Activator.CreateInstance(typeof(T), History.Empty) as T;
            if (aggregate == null) {
                throw new AggregateInstantiationException(typeof(T));
            }

            aggregate.AggregateId = aggregateId;
            aggregate.RaiseEvent(creationEvent);
            return aggregate;
        }

        private int GetNextSequence() {
            return ++_lastSequenceNumber;
        }

        protected void Apply(IDomainEvent @event)
            => State.Mutate(@event);
        protected void Apply(IEnumerable<IDomainEvent> events)
            => State.Mutate(events);

        private void HydrateFrom(History history)
        {
            if (history == default) throw new ArgumentNullException(nameof(history));

            foreach (var domainEvent in history.GetStream()) {
                AggregateId = domainEvent.AggregateId;
                _lastSequenceNumber = domainEvent.Sequence;
                Apply(domainEvent);
            }
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            ((IEventIdentifiers) @event).Set(AggregateId, GetNextSequence());
            UncommittedEvents.Add(@event);
            Apply(@event);
        }
    }
}
