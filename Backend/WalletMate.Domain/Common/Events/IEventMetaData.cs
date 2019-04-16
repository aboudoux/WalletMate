using System;

namespace WalletMate.Domain.Common.Events
{
    public interface IEventMetaData 
    {
        void SetIdentifiers(string aggregateId, int sequence);
        void SetCreationInfos(string userName, DateTimeOffset creationDate);
    }
}