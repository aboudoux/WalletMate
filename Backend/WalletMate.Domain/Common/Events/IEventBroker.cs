using System;
using System.Threading.Tasks;

namespace WalletMate.Domain.Common.Events
{
    public interface IEventBroker 
    {
        Task Publish(UncommittedAggregates aggregates);
        Task Publish(UncommittedEvents events);
        Task<T> GetAggregate<T>(string aggregateId);
        Task<T> GetAggregate<T>(Predicate<IDomainEvent> eventSelector);
    }
}