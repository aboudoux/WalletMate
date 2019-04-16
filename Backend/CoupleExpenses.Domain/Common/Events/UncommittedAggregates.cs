using System.Collections.Generic;

namespace WalletMate.Domain.Common.Events
{
    public class UncommittedAggregates 
    {
        private readonly List<IAggregateRoot> _uncommittedAggregateRoots = new List<IAggregateRoot>();

        public void Add(IAggregateRoot aggregate) => _uncommittedAggregateRoots.Add(aggregate);

        public IReadOnlyList<IAggregateRoot> GetAll() => _uncommittedAggregateRoots;        
    }
}