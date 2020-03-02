using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Actions
{
	public class Connected : LoginAction
	{
		public Connected(Pair pair) : base(pair)
		{
		}
	}
}