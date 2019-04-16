using System;
using System.Threading.Tasks;
using MediatR;
using WalletMate.Domain.Common.Events;

namespace WalletMate.Infrastructure
{
    public class MediatrEventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;

        public MediatrEventDispatcher(IMediator mediator) 
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            await _mediator.Publish(@event);
        }
    }
}