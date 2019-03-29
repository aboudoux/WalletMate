using System;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using Mediator.Net;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Infrastructure
{
    public class MediatorQueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public MediatorQueryBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TResult> QueryAsync<TResult>(IRequest query)             
        {
            var executeQuery = await _mediator.RequestAsync<IRequest, QueryResult<TResult>>(query);
            return executeQuery.Result;
        }
    }
}