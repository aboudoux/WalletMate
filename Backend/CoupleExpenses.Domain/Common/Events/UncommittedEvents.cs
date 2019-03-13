using System.Collections.Generic;

namespace CoupleExpenses.Domain.Common.Events
{
    public class UncommittedEvents : EventStream {
        public UncommittedEvents() {
        }

        public UncommittedEvents(IEnumerable<IDomainEvent> events) : base(events) {
        }
    }
}