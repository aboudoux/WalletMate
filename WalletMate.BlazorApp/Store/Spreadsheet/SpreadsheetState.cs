using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;
using WalletMate.Domain.Periods;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Spreadsheet {
	public class SpreadsheetState : State<SpreadsheetState>
	{
		public Dictionary<string, PeriodState> Periods = new Dictionary<string, PeriodState>();

		public override void Initialize()
		{
			Periods.Add(PeriodName.From(1,2020).ToString(), new PeriodState());
			Periods.Add(PeriodName.From(2,2020).ToString(), new PeriodState());
			Periods.Add(PeriodName.From(3,2020).ToString(), new PeriodState());
		}

		public class ToggleExpand : IAction
		{
			public ToggleExpand(string periodName)
			{
				PeriodName = periodName;
			}

			public string PeriodName { get; }
		}
	}

	public class PeriodState
	{
		public bool IsExpanded { get; set; }
	}
}
