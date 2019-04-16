using System;
using System.Threading;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;

namespace WalletMate.Application
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

        public async Task Handle(ReplayAllEvents context, CancellationToken cancellationToken)
        {
            var allEvents = await _eventStore.GetEvents(a => true);
            await allEvents.ForEachAsync(async (e) => await _dispatcher.Dispatch(e));
        }
    }
}