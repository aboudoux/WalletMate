using System;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application
{
    public class StartupCommandHandler : ICommandHandler<ReplayAllEvents>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventDispatcher _dispatcher;

        public StartupCommandHandler(IEventStore eventStore, IEventDispatcher dispatcher)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public async Task Handle(ReceiveContext<ReplayAllEvents> context, CancellationToken cancellationToken)
        {
            var allEvents = await _eventStore.GetEvents(a => true);
            await allEvents.ForEachAsync(async (e) => await _dispatcher.Dispatch<IDomainEvent>(e));
        }
    }
}