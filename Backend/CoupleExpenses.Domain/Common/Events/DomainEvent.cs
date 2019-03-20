using Mediator.Net.Contracts;

namespace CoupleExpenses.Domain.Common.Events
{
    public abstract class DomainEvent : IDomainEvent, IEventIdentifiers, IEvent, ISerializableType
    {
        public string AggregateId { get; private set; }
        public int Sequence { get; private set; }

        void IEventIdentifiers.Set(string aggregateId, int sequence)
        {
            AggregateId = aggregateId;
            Sequence = sequence;
        }
    }
}