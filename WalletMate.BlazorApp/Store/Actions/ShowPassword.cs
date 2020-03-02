using BlazorState;
using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Actions
{
	public class ShowPassword : LoginAction
	{
		public ShowPassword(Pair pair) : base(pair)
		{
		}
	}

	public abstract class LoginAction : IAction
	{
		public Pair Pair { get; }

		protected LoginAction(Pair pair)
		{
			Pair = pair;
		}
	}
}