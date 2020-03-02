using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Actions
{
	public class HidePassword : LoginAction
	{
		public HidePassword(Pair pair) : base(pair)
		{
		}
	}
}