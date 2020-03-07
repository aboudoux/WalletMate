using System.ComponentModel.DataAnnotations;

namespace WalletMate.BlazorApp.Data
{
	public class NewPeriodModel
	{
		[Required]
		[Range(2000,2100)]
		public int Year { get; set; }

		[Required]
		[Range(1, 12)]
		public int Month { get; set; }
	}
}