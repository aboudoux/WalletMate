using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public abstract class DomainEvent : IDomainEvent, IEventIdentifiers
    {
        public Guid AggregateId { get; private set; }
        public int Sequence { get; private set; }

        void IEventIdentifiers.Set(Guid aggregateId, int sequence)
        {
            AggregateId = aggregateId;
            Sequence = sequence;
        }
    }
}