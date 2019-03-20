using System.Threading.Tasks;
using Mediator.Net;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Core
{
    public class MediatorCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        public MediatorCommandBus()
        {
            var mediaBuilder = new MediatorBuilder();
            _mediator = mediaBuilder.RegisterHandlers(typeof(MediatorCommandBus).Assembly).Build();
        }

        public async Task SendAsync(ICommand command)
        {
            await _mediator.SendAsync(command);
        }
    }
}