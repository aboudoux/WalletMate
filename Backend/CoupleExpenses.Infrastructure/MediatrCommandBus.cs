using System;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using MediatR;

namespace CoupleExpenses.Infrastructure
{
    public class MediatrCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        public MediatrCommandBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SendAsync(ICommand command)
        {
            await _mediator.Publish(command);
        }
    }
}