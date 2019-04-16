using System;
using System.Threading.Tasks;
using MediatR;
using WalletMate.Application.Core;

namespace WalletMate.Infrastructure
{
    public class MediatrQueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public MediatrQueryBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return await _mediator.Send(query);
        }
    }
}