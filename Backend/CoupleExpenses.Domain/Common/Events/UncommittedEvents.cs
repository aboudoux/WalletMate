using System.Collections.Generic;

namespace CoupleExpenses.Domain.Common.Events
{
    public sealed class UncommittedEvents : EventStream {
        public UncommittedEvents() {
        }

        public UncommittedEvents(IEnumerable<IDomainEvent> events) : base(events) {
        }
    }
}