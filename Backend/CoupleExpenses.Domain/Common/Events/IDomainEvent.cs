using System;
using System.Threading;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IDomainEvent : IEvent
    {
        string AggregateId { get;  }
        int Sequence { get;  }
    }
}