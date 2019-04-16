using System.Collections.Generic;

namespace WalletMate.Domain.Common.Events
{
    public sealed class History : EventStream {
        public static History Empty => new History();

        public History() {
        }

        public History(IReadOnlyList<IDomainEvent> source) {
            foreach (var domainEvent in source) {
                Add(domainEvent);
            }
        }
    }
}