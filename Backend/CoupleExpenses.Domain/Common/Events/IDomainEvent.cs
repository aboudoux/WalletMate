using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IDomainEvent
    {
        Guid AggregateId { get;  }
        int Sequence { get;  }
    }
}