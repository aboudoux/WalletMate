using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using WalletMate.BlazorApp.Store.Actions;

namespace WalletMate.BlazorApp.Store.Login
{
	public class ShowPasswordReducer : ActionHandler<LoginState.ShowPassword>
	{
		private LoginState State => Store.GetState<LoginState>();

		public ShowPasswordReducer(IStore aStore) : base(aStore)
		{
		}

		public override Task<Unit> Handle(LoginState.ShowPassword action, CancellationToken aCancellationToken)
		{
			State.SetVisiblePassword(action.Pair);
			return Unit.Task;
		}
	}

	public class NotifyBadLoginReducer : ActionHandler<NotifyBadLogin>
	{
		private LoginState State => Store.GetState<LoginState>();

		public NotifyBadLoginReducer(IStore aStore) : base(aStore) {
		}

		public override Task<Unit> Handle(NotifyBadLogin action, CancellationToken aCancellationToken) {

			State.CurrentPairState(action.Pair).BadPassword = true;
			return Unit.Task;
		}


		public static LoginState Handle(LoginState state, BadPasswordNotified action)
		{
			state.CurrentPairState(action.Pair).BadPassword = false;
			return state;
		}

		public static LoginState Handle(LoginState state, HidePassword action)
		{
			state.CurrentPairState(action.Pair).VisiblePassword = false;
			return state;
		}

		
		public static LoginState Handle(LoginState state, Disconnect action)
		{
			state.IsConnected = false;
			return state;
		}

		public static LoginState Handle(LoginState state, Connected action) {
			state.IsConnected = true;
			return state;
		}
	}

	public class ConfiguredPairRetrievedReducer : ActionHandler<LoginState.ConfiguredPairRetrieved>
	{
		private LoginState State => Store.GetState<LoginState>();

		public ConfiguredPairRetrievedReducer(IStore aStore) : base(aStore)
		{
		}

		public override Task<Unit> Handle(LoginState.ConfiguredPairRetrieved action, CancellationToken aCancellationToken)
		{
			State.FirstPair.PairName = action.ConfiguredPair.FirstPairName;
			State.SecondPair.PairName = action.ConfiguredPair.SecondPairName;
			State.PairRetrieved = true;
			return Unit.Task;
		}
	}
}