using System;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common.Events;
using Mediator.Net;
using Mediator.Net.Pipeline;

namespace CoupleExpenses.Infrastructure
{
    public class MediatorEventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;

        public MediatorEventDispatcher(IMediator mediator) 
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            try
            {
                await _mediator.PublishAsync(@event);
            }
            catch (NoHandlerFoundException)
            {
            }
        }
    }
}