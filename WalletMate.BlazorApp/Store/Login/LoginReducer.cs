using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using WalletMate.BlazorApp.Store.Spreadsheet;

namespace WalletMate.BlazorApp.Store.Login
{
	public class LoginReducer : ActionHandler<LoginState.ShowPassword>,
		IRequestHandler<LoginState.NotifyBadLogin>,
		IRequestHandler<LoginState.ConfiguredPairRetrieved>,
		IRequestHandler<LoginState.Connected>,
		IRequestHandler<LoginState.HidePassword>,
		IRequestHandler<LoginState.Disconnect>,
		IRequestHandler<LoginState.BadLoginNotified>
	{
		private readonly IMediator _mediator;
		private LoginState State => Store.GetState<LoginState>();

		public LoginReducer(IStore aStore, IMediator mediator) : base(aStore)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

		public Task<Unit> Handle(LoginState.Connected action, CancellationToken cancellationToken)
		{
			State.CurrentPairState(action.Pair).VisiblePassword = false;
			State.IsConnected = true;
			return _mediator.Send(new SpreadsheetState.RetrieveAllPeriods());
		}

		public Task<Unit> Handle(LoginState.HidePassword action, CancellationToken cancellationToken)
		{
			State.CurrentPairState(action.Pair).VisiblePassword = false;
			return Unit.Task;
		}

		public Task<Unit> Handle(LoginState.Disconnect action, CancellationToken cancellationToken)
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