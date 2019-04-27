using System;
using System.Threading;
using System.Threading.Tasks;
using WalletMate.Application.Core;
using WalletMate.Application.Pairs;
using WalletMate.Domain.Common.Events;
using WalletMate.Domain.Periods.Events;
using WalletMate.Domain.Periods.ValueObjects;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Services;

namespace WalletMate.Infrastructure.Projections {

    public class OperationProjections : 
        IEventHandler<PeriodCreated>,
        IEventHandler<SpendingAdded>,
        IEventHandler<RecipeAdded>,
        IEventHandler<AmountChanged>,
        IEventHandler<SpendingRemoved>,
        IEventHandler<RecipeRemoved>,
        IEventHandler<PeriodBalanceChanged>,
        IEventHandler<LabelChanged>,
        IEventHandler<PairChanged>,
        IEventHandler<RecipeCategoryChanged>,
        IEventHandler<SpendingCategoryChanged>
    {
        private readonly IUserProvider _userProvider;
        private readonly IDatabaseRepository _databaseRepository;

        public OperationProjections(IDatabaseRepository databaseRepository, IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(_databaseRepository));
        }

        public Task Handle(PeriodCreated @event, CancellationToken cancellationToken)
        {
            _databaseRepository.AddPeriod(@event.PeriodName);
            return Task.CompletedTask;
        }

        public Task Handle(SpendingAdded @event, CancellationToken cancellationToken)
        {
            return AddOperation(new PeriodOperation(@event, @event.Pair.GetUserName(_userProvider)));
        }

        public Task Handle(RecipeAdded @event, CancellationToken cancellationToken)
        {
            return AddOperation(new PeriodOperation(@event, @event.Pair.GetUserName(_userProvider)));
        }

        public Task Handle(SpendingRemoved @event, CancellationToken cancellationToken)
        {
            return RemoveOperation(PeriodId.From(@event.AggregateId), @event.OperationId);
        }

        public Task Handle(RecipeRemoved @event, CancellationToken cancellationToken)
        {
            return RemoveOperation(PeriodId.From(@event.AggregateId), @event.OperationId);
        }

        private Task AddOperation(PeriodOperation operation)
        {
            _databaseRepository.AddOperation(operation);
            return Task.CompletedTask;
        }

        private Task RemoveOperation(PeriodId periodId, OperationId operationId)
        {
            _databaseRepository.RemoveOperation(periodId, operationId);
            return Task.CompletedTask;
        }

        public Task Handle(PeriodBalanceChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateBalance(PeriodId.From(@event.AggregateId), @event.AmountDue, @event.By);
            return Task.CompletedTask;
        }

        public Task Handle(AmountChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateOperation(PeriodId.From(@event.AggregateId), @event.OperationId, @event.Amount);
            return Task.CompletedTask;
        }

        public Task Handle(LabelChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateOperation(PeriodId.From(@event.AggregateId), @event.OperationId, label: @event.Label);
            return Task.CompletedTask;
        }

        public Task Handle(PairChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateOperation(PeriodId.From(@event.AggregateId), @event.OperationId, pair: @event.Pair);
            return Task.CompletedTask;
        }

        public Task Handle(RecipeCategoryChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateOperation(PeriodId.From(@event.AggregateId), @event.OperationId, recipeCategory : @event.Category);
            return Task.CompletedTask;
        }

        public Task Handle(SpendingCategoryChanged @event, CancellationToken cancellationToken)
        {
            _databaseRepository.UpdateOperation(PeriodId.From(@event.AggregateId), @event.OperationId, spendingCategory: @event.Category);
            return Task.CompletedTask;
        }
    }
}
