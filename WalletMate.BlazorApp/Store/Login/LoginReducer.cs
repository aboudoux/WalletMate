using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

namespace WalletMate.BlazorApp.Store.Login
{
	public class RootReducer : ActionHandler<LoginState.ShowPassword>,
		IRequestHandler<LoginState.NotifyBadLogin>,
		IRequestHandler<LoginState.ConfiguredPairRetrieved>,
		IRequestHandler<LoginState.Connected>,
		IRequestHandler<LoginState.HidePassword>,
		IRequestHandler<LoginState.Disconnect>,
		IRequestHandler<LoginState.BadLoginNotified>
	{
		private LoginState State => Store.GetState<LoginState>();

		public RootReducer(IStore aStore) : base(aStore)
		{
		}

		public override Task<Unit> Handle(LoginState.ShowPassword action, CancellationToken aCancellationToken)
		{
			State.SetVisiblePassword(action.Pair);
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.NotifyBadLogin action, CancellationToken cancellationToken)
		{
			State.CurrentPairState(action.Pair).BadPassword = true;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.ConfiguredPairRetrieved action, CancellationToken cancellationToken)
		{
			State.FirstPair.PairName = action.ConfiguredPair.FirstPairName;
			State.SecondPair.PairName = action.ConfiguredPair.SecondPairName;
			State.PairRetrieved = true;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.Connected request, CancellationToken cancellationToken)
		{
			State.IsConnected = true;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.HidePassword action, CancellationToken cancellationToken)
		{
			State.CurrentPairState(action.Pair).VisiblePassword = false;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.Disconnect request, CancellationToken cancellationToken)
		{
			State.IsConnected = false;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.BadLoginNotified action, CancellationToken cancellationToken)
		{
			State.CurrentPairState(action.Pair).BadPassword = false;
			return Unit.Task;
		}
	}
}