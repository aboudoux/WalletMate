using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Data
{
	public class NewSpendingModel : NewOperationModel
	{
		public SpendingCategory Category { get; set; }
	}
}