using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;
using WalletMate.Application.Periods.Queries;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Spreadsheet {
	public class SpreadsheetState : State<SpreadsheetState>
	{
		public bool AddPeriodPanelVisible { get; set; }
		public bool PeriodMenuVisible { get; set; }

		public Dictionary<PeriodId, PeriodState> Periods = new Dictionary<PeriodId, PeriodState>();

		public override void Initialize()
		{
		}

		public void SetPeriods(IReadOnlyList<IPeriodResult> periods)
		{
			AddPeriodPanelVisible = false;
			PeriodMenuVisible = false;
			Periods = periods.ToDictionary(a => PeriodId.From(a.PeriodId), result => new PeriodState());
		}

		public class RetrieveAllPeriods : IAction
		{
		}

		public class AllPeriodRetrieved : IAction
		{
			public IReadOnlyList<IPeriodResult> Periods { get; }

			public AllPeriodRetrieved(IReadOnlyList<IPeriodResult> periods)
			{
				Periods = periods;
			}
		}

		public class ShowAddPeriodPanel : IAction
		{
			public bool Show { get; }

			public ShowAddPeriodPanel(bool show)
			{
				Show = show;
			}
		}


		public class ToggleExpand : PeriodAction
		{
			public ToggleExpand(PeriodId periodId) : base(periodId)
			{
			}
		}

		public class RetrieveOperations : PeriodAction
		{
			public RetrieveOperations(PeriodId periodId) : base(periodId)
			{
			}
		}

		public class OperationRetrieved : PeriodAction
		{
			public IReadOnlyList<IPeriodOperation> Operations { get; }
			public OperationRetrieved(PeriodId periodId, IReadOnlyList<IPeriodOperation> operations) : base(periodId)
			{
				Operations = operations;
			}
		}

		public class ShowPeriodMenu : IAction
		{
			
		}
	}

	public class PeriodState
	{
		public enum ExpandState
		{
			Collapsed,
			Expanding,
			Expanded
		}

		public ExpandState Expand { get; set; }

		public IReadOnlyList<IPeriodOperation> Operations { get; set; }
	}

	public abstract class PeriodAction : IAction
	{
		public PeriodId PeriodId { get; }

		protected PeriodAction(PeriodId periodId)
		{
			PeriodId = periodId;
		}
	}
}
