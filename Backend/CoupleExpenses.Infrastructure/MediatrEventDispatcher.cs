using System;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common.Events;
using MediatR;

namespace CoupleExpenses.Infrastructure
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