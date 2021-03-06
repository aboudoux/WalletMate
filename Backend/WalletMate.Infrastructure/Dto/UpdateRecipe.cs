namespace WalletMate.Infrastructure.Dto
{
    public class UpdateRecipe
    {
        public string PeriodId { get; }
        public int OperationId { get; }
        public double Amount { get; }
        public string Label { get; }
        public int Pair { get; }
        public int Category { get; }

        public UpdateRecipe(string periodId, int operationId, double amount, string label, int pair, int category)
        {
            PeriodId = periodId;
            OperationId = operationId;
            Amount = amount;
            Label = label;
            Pair = pair;
            Category = category;
        }
    }
}