using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Periods.Queries {
    public class PeriodQueryHandler : 
        IRequestHandler<GetAllPeriod, QueryResult<IReadOnlyList<string>>>
    {
        private readonly IDatabaseRepository _repository;

        public PeriodQueryHandler(IDatabaseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<QueryResult<IReadOnlyList<string>>> Handle(ReceiveContext<GetAllPeriod> context, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllPeriod();
            return new QueryResult<IReadOnlyList<string>>(result);
        }
    }
}
