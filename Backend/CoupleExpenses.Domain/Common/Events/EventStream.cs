using System.Collections.Generic;
using System.Linq;

namespace CoupleExpenses.Domain.Common.Events
{
    public abstract class EventStream {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        protected EventStream() {

        }

        protected EventStream(IEnumerable<IDomainEvent> events) {
            _events.AddRange(events);
        }

        public IOrderedEnumerable<IDomainEvent> GetStream() {
            return _events.OrderBy(a => a.Sequence);
        }

        public void Add(IDomainEvent @event) {
            _events.Add(@event);
        }
    }
}