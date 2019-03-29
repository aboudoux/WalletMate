using System;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Periods.Events;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Infrastructure.Projections {

    public class OperationProjections : IEventHandler<PeriodCreated>
    {
        private readonly IDatabaseRepository _databaseRepository;

        public OperationProjections(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(_databaseRepository));
        }

        public Task Handle(IReceiveContext<PeriodCreated> context, CancellationToken cancellationToken)
        {
            _databaseRepository.AddPeriod(context.Message.PeriodName);
            return Task.CompletedTask;
        }
    }
}
