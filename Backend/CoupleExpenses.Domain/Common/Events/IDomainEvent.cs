using System;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IDomainEvent
    {
        string AggregateId { get;  }
        int Sequence { get;  }
    }
}