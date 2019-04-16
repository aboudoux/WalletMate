using System.Collections.Generic;

namespace WalletMate.Domain.Common.Events
{
    public sealed class UncommittedEvents : EventStream {
        public UncommittedEvents() {
        }

        public UncommittedEvents(IEnumerable<IDomainEvent> events) : base(events) {
        }
    }
}