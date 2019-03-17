using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IEventIdentifiers {
        void Set(string aggregateId, int sequence);
    }
}