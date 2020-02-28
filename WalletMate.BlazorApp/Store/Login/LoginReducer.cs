using Blazor.Fluxor;

namespace WalletMate.BlazorApp.Store.Login
{
	public static class LoginReducer
	{
		[ReducerMethod]
		public static LoginState Handle(LoginState state, NotifyBadLogin action)
			=> new LoginState(true);

		[ReducerMethod]
		public static LoginState Handle(LoginState state, BadPasswordNotified action)
			=> new LoginState(false);
	}
}