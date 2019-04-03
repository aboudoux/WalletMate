using System;
using MediatR;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IDomainEvent : IEvent
    {
        string AggregateId { get;  }
        int Sequence { get;  }
        
        string UserName { get; }
        DateTimeOffset CreationDate { get; }
    }   
}