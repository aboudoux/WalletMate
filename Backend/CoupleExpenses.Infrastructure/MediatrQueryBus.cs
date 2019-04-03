using System;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using MediatR;

namespace CoupleExpenses.Infrastructure
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