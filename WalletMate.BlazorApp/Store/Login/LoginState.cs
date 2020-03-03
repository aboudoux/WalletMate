using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;
using WalletMate.Application.Pairs.Queries;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Login {
	public class LoginState : State<LoginState>
	{
		public PairState FirstPair { get; set; } = PairState.Empty;
		public PairState SecondPair { get; set; } = PairState.Empty;
		public bool IsConnected { get; set; }

		public bool PairRetrieved { get; set; }

		public PairState CurrentPairState(Pair pair) => pair == Pair.First ? FirstPair : SecondPair;

		public LoginState SetVisiblePassword(Pair pair)
		{
			if (pair == Pair.First)
			{
				FirstPair.VisiblePassword = true;
				SecondPair.VisiblePassword = false;
			}
			else
			{
				FirstPair.VisiblePassword = false;
				SecondPair.VisiblePassword = true;
			}

			return this;
		}

		public override void Initialize()
		{
		}

		#region Actions
		public class ConfiguredPairRetrieved : IAction {
			public IConfiguredPair ConfiguredPair { get; }

			public ConfiguredPairRetrieved(IConfiguredPair configuredPair) {
				ConfiguredPair = configuredPair ?? throw new ArgumentNullException(nameof(configuredPair));
			}
		}

		public class ShowPassword : LoginAction {
			public ShowPassword(Pair pair) : base(pair) {
			}
		}

		public class Disconnect : IAction {

		}

		public class Connected : LoginAction {
			public Connected(Pair pair) : base(pair) {
			}
		}

		public class HidePassword : LoginAction {
			public HidePassword(Pair pair) : base(pair) {
			}
		}

		public class NotifyBadLogin : LoginAction {
			public NotifyBadLogin(Pair pair) : base(pair) {
			}
		}
		#endregion
	}

	public class PairState
	{
		public bool BadPassword { get; set; }
		public bool VisiblePassword { get; set; }
		public string PairName { get; set; }

		public PairState(bool badPassword, bool visiblePassword, string pairName)
		{
			BadPassword = badPassword;
			VisiblePassword = visiblePassword;
			PairName = pairName;
		}

		public static PairState Empty => new PairState(false, false, string.Empty);
	}
}
