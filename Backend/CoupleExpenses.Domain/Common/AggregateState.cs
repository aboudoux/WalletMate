using System;
using System.Collections.Generic;
using System.Linq;
using CoupleExpenses.Domain.Common.Events;

namespace CoupleExpenses.Domain.Common
{
    public abstract class AggregateState 
    {
        private readonly Dictionary<Type, IList<Action<IDomainEvent>>> _eventHandlers = new Dictionary<Type, IList<Action<IDomainEvent>>>();

        public void Mutate(IEnumerable<IDomainEvent> events) {
            foreach (var @event in events) {
                Mutate(@event);
            }
        }

        public void Mutate(IDomainEvent evt) {
            var eventType = evt.GetType();
            var handlers = _eventHandlers.Where(o => o.Key.IsAssignableFrom(eventType));

            foreach (var value in handlers.SelectMany(h => h.Value)) {
                value(evt);
            }
        }

        protected void AddHandler<T>(Action<T> handler)
            where T : IDomainEvent 
        {
            if (!_eventHandlers.ContainsKey(typeof(T))) {
                _eventHandlers.Add(typeof(T), new List<Action<IDomainEvent>>());
            }

            _eventHandlers[typeof(T)].Add(@event => handler((T) @event));
        }
    }
}