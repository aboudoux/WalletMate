using WalletMate.Domain.Periods.ValueObjects;

namespace WalletMate.BlazorApp.Data
{
	public class NewRecipeModel : NewOperationModel
	{
		public RecipeCategory Category { get; set; }
	}
}