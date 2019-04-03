using System;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods.Events;
using CoupleExpenses.Infrastructure.Dto;

namespace CoupleExpenses.Infrastructure.Projections {

    public class OperationProjections : 
        IEventHandler<PeriodCreated>,
        IEventHandler<SpendingAdded>,
        IEventHandler<RecipeAdded>
    {
        private readonly IDatabaseRepository _databaseRepository;

        public OperationProjections(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(_databaseRepository));
        }

        public Task Handle(PeriodCreated @event, CancellationToken cancellationToken)
        {
            _databaseRepository.AddPeriod(@event.PeriodName);
            return Task.CompletedTask;
        }

        public Task Handle(SpendingAdded @event, CancellationToken cancellationToken)
        {
            _databaseRepository.AddOperation(new PeriodOperation(@event));
            return Task.CompletedTask;
        }

        public Task Handle(RecipeAdded @event, CancellationToken cancellationToken)
        {
            _databaseRepository.AddOperation(new PeriodOperation(@event));
            return Task.CompletedTask;
        }
    }
}
