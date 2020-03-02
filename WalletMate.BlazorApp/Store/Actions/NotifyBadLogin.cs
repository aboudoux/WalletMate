using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Actions
{
	public class NotifyBadLogin : LoginAction
	{
		public NotifyBadLogin(Pair pair) : base(pair)
		{
		}
	}
}