using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IEventMetaData 
    {
        void SetIdentifiers(string aggregateId, int sequence);
        void SetCreationInfos(string userName, DateTimeOffset creationDate);
    }
}