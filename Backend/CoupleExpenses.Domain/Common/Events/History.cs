using System.Collections.Generic;

namespace CoupleExpenses.Domain.Common.Events
{
    public class History : EventStream {
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