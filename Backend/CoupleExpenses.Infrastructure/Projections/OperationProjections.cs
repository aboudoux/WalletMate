using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Periods.Events;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Infrastructure.Projections {

    public class OperationProjections : IEventHandler<PeriodCreated>
    {
        public Task Handle(IReceiveContext<PeriodCreated> context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
