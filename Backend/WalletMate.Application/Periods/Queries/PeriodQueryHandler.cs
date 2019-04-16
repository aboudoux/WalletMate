using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WalletMate.Application.Core;

namespace WalletMate.Application.Periods.Queries {
    public class PeriodQueryHandler : 
        IQueryHandler<GetAllPeriod, IReadOnlyList<IPeriodResult>>,
        IQueryHandler<GetAllOperation, IReadOnlyList<IPeriodOperation>>,
        IQueryHandler<GetPeriodBalance, IPeriodBalance>
    {
        private readonly IDatabaseRepository _repository;

        public PeriodQueryHandler(IDatabaseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IReadOnlyList<IPeriodResult>> Handle(GetAllPeriod request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPeriod();
        }

        public async Task<IReadOnlyList<IPeriodOperation>> Handle(GetAllOperation request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllOperation(request.PeriodId);
        }

        public async Task<IPeriodBalance> Handle(GetPeriodBalance request, CancellationToken cancellationToken)
        {
            return await _repository.GetBalance(request.PeriodId);
        }
    }
}
