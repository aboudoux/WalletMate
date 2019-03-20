using System;
using System.Threading.Tasks;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IEventStore 
    {
        Task<IDomainEvent[]> GetEvents(Predicate<IDomainEvent> predicate);
        Task Save(IDomainEvent @event);
        Task<int> GetLastSequence(string aggregateId);
    }
}