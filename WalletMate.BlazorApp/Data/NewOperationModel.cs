using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Data
{
	public abstract class NewOperationModel
	{
		public string Label { get; set; }
		public string Amount { get; set; }
		public Pair Pair { get; set; }
	}
}