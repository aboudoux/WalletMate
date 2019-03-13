using System;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Common.Exceptions;

namespace CoupleExpenses.Domain.Common {
    public abstract class AggregateRoot {
        private readonly EventPlayer _player = new EventPlayer();
        public Guid AggregateId { get; protected set; }
        private int _lastSequenceNumber = -1;

        public UncommittedEvents UncommittedEvents { get; } = new UncommittedEvents();

        protected void AddPlayer<T>(Action<T> action) => _player.Add(action);

        protected static T CreateNew<T>(Guid aggregateId, IDomainEvent creationEvent) where T : AggregateRoot {
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

        protected void Apply<T>(T @event) where T : IDomainEvent
            => _player.Apply(@event);

        protected void HydrateFrom(History history) {
            if (history == default) throw new ArgumentNullException(nameof(history));
            if (!_player.HasHandlers) throw new NoPlayersFoundException();

            foreach (var domainEvent in history.GetStream()) {
                AggregateId = domainEvent.AggregateId;
                _lastSequenceNumber = domainEvent.Sequence;
                _player.Apply(domainEvent);
            }
        }

        protected void RaiseEvent(IDomainEvent @event) {
            ((IEventIdentifiers) @event).Set(AggregateId, GetNextSequence());
            UncommittedEvents.Add(@event);
            Apply(@event);
        }
    }
}
