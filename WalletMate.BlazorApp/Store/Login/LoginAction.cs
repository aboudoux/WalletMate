using BlazorState;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Login
{
	public abstract class LoginAction : IAction
	{
		public Pair Pair { get; }

		protected LoginAction(Pair pair)
		{
			Pair = pair;
		}
	}
}