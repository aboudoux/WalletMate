using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IEventIdentifiers {
        void Set(Guid aggregateId, int sequence);
    }
}