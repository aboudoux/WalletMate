using System;
using System.Threading;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.Domain.Periods;
using CoupleExpenses.Domain.Periods.Events;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace CoupleExpenses.Application.Periods
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

        public async Task Handle(ReceiveContext<CreatePeriod> context, CancellationToken cancellationToken)
        {
            var command = context.Message;

            var periodCreator = await _eventBroker.GetAggregate<PeriodCreator>(a => a is PeriodCreated);
            
            var period = periodCreator.Create(command.PeriodName);

            await _eventBroker.Publish(period.UncommittedEvents);
        }

        public Task Handle(ReceiveContext<AddSpending> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ReceiveContext<ChangeSpending> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ReceiveContext<AddRecipe> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ReceiveContext<ChangeRecipe> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ReceiveContext<RemoveOperation> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}