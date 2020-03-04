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
		public Dictionary<PeriodId, PeriodState> Periods = new Dictionary<PeriodId, PeriodState>();

		public override void Initialize()
		{
			Periods.Add(PeriodId.From(1,2020), new PeriodState());
			Periods.Add(PeriodId.From(2,2020), new PeriodState());
			Periods.Add(PeriodId.From(3,2020), new PeriodState());
		}

		public class ToggleExpand : PeriodAction
		{
			public ToggleExpand(PeriodId periodId) : base(periodId)
			{
			}
		}

		public class RetrieveData : PeriodAction
		{
			public RetrieveData(PeriodId periodId) : base(periodId)
			{
			}
		}

		public class DataRetrieved : PeriodAction
		{
			public IReadOnlyList<IPeriodOperation> Operations { get; }
			public DataRetrieved(PeriodId periodId, IReadOnlyList<IPeriodOperation> operations) : base(periodId)
			{
				Operations = operations;
			}
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
