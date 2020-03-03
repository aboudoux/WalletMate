using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

namespace WalletMate.BlazorApp.Store.Spreadsheet
{
	public class SpreadsheetReducer : ActionHandler<SpreadsheetState.ToggleExpand>
	{
		private SpreadsheetState State => Store.GetState<SpreadsheetState>();

		public SpreadsheetReducer(IStore aStore) : base(aStore)
		{
		}

		public override Task<Unit> Handle(SpreadsheetState.ToggleExpand action, CancellationToken aCancellationToken)
		{
			State.Periods[action.PeriodName].IsExpanded = !State.Periods[action.PeriodName].IsExpanded;
			return Unit.Task;
		}
	}
}