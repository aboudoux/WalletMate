using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using WalletMate.Application.Core;
using WalletMate.Application.Periods;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Spreadsheet
{
	public class SpreadsheetReducer : ActionHandler<SpreadsheetState.ToggleExpand>,
		IRequestHandler<SpreadsheetState.RetrieveOperations>,
		IRequestHandler<SpreadsheetState.OperationRetrieved>,
		IRequestHandler<SpreadsheetState.RetrieveAllPeriods>,
		IRequestHandler<SpreadsheetState.AllPeriodRetrieved>,
		IRequestHandler<SpreadsheetState.ShowAddPeriodPanel>,
		IRequestHandler<SpreadsheetState.ShowPeriodMenu>,
		IRequestHandler<SpreadsheetState.CreatePeriod>
	{
		private readonly IMediator _mediator;
		private readonly IQueryBus _queryBus;
		private readonly ICommandBus _commandBus;
		private SpreadsheetState State => Store.GetState<SpreadsheetState>();

		public SpreadsheetReducer(IStore aStore, IMediator mediator, IQueryBus queryBus, ICommandBus commandBus) : base(aStore)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
			_commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
		}

		public override Task<Unit> Handle(SpreadsheetState.ToggleExpand action, CancellationToken aCancellationToken)
		{
			var expand = State.Periods[action.PeriodId].Expand;
			if (expand == PeriodState.ExpandState.Expanded)
				State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Collapsed;
			else
				return _mediator.Send(new SpreadsheetState.RetrieveOperations(action.PeriodId));

			return Unit.Task;
		}

		public async Task<Unit> Handle(SpreadsheetState.RetrieveOperations action, CancellationToken cancellationToken)
		{
			State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Expanding;
			var operations =  await _queryBus.QueryAsync(new GetAllOperation(action.PeriodId));
			await _mediator.Send(new SpreadsheetState.OperationRetrieved(action.PeriodId, operations));
			return Unit.Value;
		}

		public Task<Unit> Handle(SpreadsheetState.OperationRetrieved action, CancellationToken cancellationToken)
		{
			State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Expanded;
			State.Periods[action.PeriodId].Operations = action.Operations;
			return Unit.Task;
		}

		public async Task<Unit> Handle(SpreadsheetState.RetrieveAllPeriods action, CancellationToken cancellationToken)
		{
			var periods = await _queryBus.QueryAsync(new GetAllPeriod());
			await _mediator.Send(new SpreadsheetState.AllPeriodRetrieved(periods));
			return Unit.Value;
		}

		public Task<Unit> Handle(SpreadsheetState.AllPeriodRetrieved action, CancellationToken cancellationToken)
		{
			State.SetPeriods(action.Periods);
			return Unit.Task;
		}

		public Task<Unit> Handle(SpreadsheetState.ShowAddPeriodPanel action, CancellationToken cancellationToken)
		{
			State.AddPeriodPanelVisible = action.Show;
			State.PeriodMenuVisible = false;
			return Unit.Task;
		}

		public Task<Unit> Handle(SpreadsheetState.ShowPeriodMenu action, CancellationToken cancellationToken)
		{
			State.PeriodMenuVisible = true;
			return Unit.Task;
		}

		public async Task<Unit> Handle(SpreadsheetState.CreatePeriod action, CancellationToken cancellationToken)
		{
			await _commandBus.SendAsync(new CreatePeriod(PeriodName.From(action.Month, action.Year)));
			await _mediator.Send(new SpreadsheetState.RetrieveAllPeriods());
			return Unit.Value;
		}
	}
}