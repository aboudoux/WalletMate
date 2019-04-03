using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;

namespace CoupleExpenses.Application.Periods.Queries {
    public class PeriodQueryHandler : 
        IQueryHandler<GetAllPeriod, IReadOnlyList<string>>
    {
        private readonly IDatabaseRepository _repository;

        public PeriodQueryHandler(IDatabaseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IReadOnlyList<string>> Handle(GetAllPeriod request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPeriod();
        }
    }
}
