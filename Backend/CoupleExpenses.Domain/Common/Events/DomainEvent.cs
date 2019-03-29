using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public abstract class DomainEvent : IDomainEvent, IEventMetaData,  ISerializableType
    {
        public string AggregateId { get; private set; }
        public int Sequence { get; private set; }
        public string UserName { get; private set; }
        public DateTimeOffset CreationDate { get; private set; }

        void IEventMetaData.SetIdentifiers(string aggregateId, int sequence)
        {
            AggregateId = aggregateId;
            Sequence = sequence;            
        }

        void IEventMetaData.SetCreationInfos(string userName, DateTimeOffset creationDate)
        {
            UserName = userName;
            CreationDate = creationDate;
        }
    }
}