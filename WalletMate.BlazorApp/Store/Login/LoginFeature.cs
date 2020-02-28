using Blazor.Fluxor;

namespace WalletMate.BlazorApp.Store.Login
{
	public class LoginFeature : Feature<LoginState>
	{
		public override string GetName() => "Login";

		protected override LoginState GetInitialState() => new LoginState(false);
	}
}