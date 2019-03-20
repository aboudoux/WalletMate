using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoupleExpenses.Domain.Common.Events
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }

    public class UncommittedAggregates 
    {
        private readonly List<IAggregateRoot> _uncommittedAggregateRoots = new List<IAggregateRoot>();

        public void Add(IAggregateRoot aggregate) => _uncommittedAggregateRoots.Add(aggregate);

        public IReadOnlyList<IAggregateRoot> GetAll() => _uncommittedAggregateRoots;        
    }
}