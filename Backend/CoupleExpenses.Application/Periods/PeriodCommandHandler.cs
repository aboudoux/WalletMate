using System;
using System.Threading;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Domain.Common;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Common.Exceptions;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.Events;

namespace WalletMate.Application.Periods
{
    public class PeriodCommandHandler : 
        ICommandHandler<CreatePeriod>, 
        ICommandHandler<AddSpending>,
        ICommandHandler<ChangeSpending>,
        ICommandHandler<AddRecipe>,
        ICommandHandler<ChangeRecipe>,
        ICommandHandler<RemoveOperation>
    {
        private readonly IEventBroker _eventBroker;

        public PeriodCommandHandler(IEventBroker eventBroker)
        {
            _eventBroker = eventBroker ?? throw new ArgumentNullException(nameof(eventBroker));
        }

        public async Task Handle(CreatePeriod command, CancellationToken cancellationToken)
        {
            PeriodCreator periodCreator;

            try
            {
                periodCreator = await _eventBroker.GetAggregate<PeriodCreator>(a => a is PeriodCreated);
            }
            catch (AggregateNotFoundException)
            {
                periodCreator = new PeriodCreator(History.Empty);
            }

            var period = periodCreator.Create(command.PeriodName);
            await _eventBroker.Publish(period.UncommittedEvents);
        }

        public async Task Handle(AddSpending command, CancellationToken cancellationToken)
        {
            await (await LoadAggregate<Period>(command.PeriodId.ToString()))
                .AndExecute(p => p.AddSpending(command.Amount, command.Label, command.Pair, command.Category));
        }

        public async Task Handle(ChangeSpending command, CancellationToken cancellationToken)
        {
            await (await LoadAggregate<Period>(command.PeriodId.ToString()))
                .AndExecute(p => p.ChangeSpending(command.OperationId, command.Amount, command.Label, command.Pair, command.Category));
        }

        public async Task Handle(AddRecipe command, CancellationToken cancellationToken)
        {
            await (await LoadAggregate<Period>(command.PeriodId.ToString()))
                .AndExecute(p => p.AddRecipe(command.Amount, command.Label, command.Pair, command.Category));
        }

        public async Task Handle(ChangeRecipe command, CancellationToken cancellationToken)
        {
            await (await LoadAggregate<Period>(command.PeriodId.ToString()))
                .AndExecute(p => p.ChangeRecipe(command.OperationId, command.Amount, command.Label, command.Pair, command.Category));
        }

        public async Task Handle(RemoveOperation command, CancellationToken cancellationToken)
        {
            await (await LoadAggregate<Period>(command.PeriodId.ToString()))
                .AndExecute(p => p.RemoveOperation(command.OperationId));
        }

        private async Task<AggregateExecutor<T>> LoadAggregate<T>(string aggregateId)
            where T : IAggregateRoot
        {
            var aggregate = await _eventBroker.GetAggregate<T>(aggregateId);
            return new AggregateExecutor<T>(_eventBroker, aggregate);
        }
    }

   
    public class AggregateExecutor<T>
        where T : IAggregateRoot
    {
        private readonly IEventBroker _broker;
        private readonly T _loadedAggregate;

        public AggregateExecutor(IEventBroker broker, T loadedAggregate)            
        {
            _broker = broker ?? throw new ArgumentNullException(nameof(broker));
            _loadedAggregate = loadedAggregate;
        }

        public async Task AndExecute(Action<T> action)
        {
            action(_loadedAggregate);
            await _broker.Publish(_loadedAggregate.UncommittedEvents);
        }
    }
}