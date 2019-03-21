using System;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using Mediator.Net;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Infrastructure
{
    public class MediatorCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        public MediatorCommandBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SendAsync(ICommand command)
        {
            await _mediator.SendAsync(command);
        }
    }
}