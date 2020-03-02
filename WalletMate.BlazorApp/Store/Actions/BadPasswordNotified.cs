using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Store.Actions
{
	public class BadPasswordNotified : LoginAction
	{
		public BadPasswordNotified(Pair pair) : base(pair)
		{
		}
	}
}