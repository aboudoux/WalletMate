using System;
using System.Collections.Generic;
using System.Linq;

namespace WalletMate.Domain.Common.Events
{
    public class EventPlayer {
        private readonly Dictionary<Type, Action<object>> _handlers = new Dictionary<Type, Action<object>>();

        public EventPlayer Add<T>(Action<T> apply) {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
                throw new ArgumentException($"The type {type} is already registered in this event player");
            _handlers.Add(type, a => apply((T) a));

            return this;
        }

        public void Apply<T>(T @event) where T : IDomainEvent {
            var type = @event.GetType();

            if (_handlers.ContainsKey(type)) {
                _handlers[type](@event);
            }
        }

        public bool HasHandlers => _handlers.Any();

    }
}