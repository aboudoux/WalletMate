using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using WalletMate.Application.Core;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Spreadsheet
{
	public class SpreadsheetReducer : ActionHandler<SpreadsheetState.ToggleExpand>,
		IRequestHandler<SpreadsheetState.RetrieveData>,
		IRequestHandler<SpreadsheetState.DataRetrieved>
	{
		private readonly IMediator _mediator;
		private readonly IQueryBus _queryBus;
		private SpreadsheetState State => Store.GetState<SpreadsheetState>();

		public SpreadsheetReducer(IStore aStore, IMediator mediator, IQueryBus queryBus) : base(aStore)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
		}

		public override Task<Unit> Handle(SpreadsheetState.ToggleExpand action, CancellationToken aCancellationToken)
		{
			var expand = State.Periods[action.PeriodId].Expand;
			if (expand == PeriodState.ExpandState.Expanded)
				State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Collapsed;
			else
				return _mediator.Send(new SpreadsheetState.RetrieveData(action.PeriodId));

			return Unit.Task;
		}

		public async Task<Unit> Handle(SpreadsheetState.RetrieveData action, CancellationToken cancellationToken)
		{
			State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Expanding;
			var operations =  await _queryBus.QueryAsync(new GetAllOperation(action.PeriodId));
			await _mediator.Send(new SpreadsheetState.DataRetrieved(action.PeriodId, operations));
			return Unit.Value;
		}

		public Task<Unit> Handle(SpreadsheetState.DataRetrieved action, CancellationToken cancellationToken)
		{
			State.Periods[action.PeriodId].Expand = PeriodState.ExpandState.Expanded;
			State.Periods[action.PeriodId].Operations = action.Operations;
			return Unit.Task;
		}
	}
}