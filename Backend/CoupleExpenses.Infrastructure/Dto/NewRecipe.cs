namespace CoupleExpenses.Infrastructure.Dto
{
    public class NewRecipe
    {
        public NewRecipe(string periodId, double amount, string label, int pair, int operationType)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            OperationType = operationType;
        }

        public string PeriodId { get; }
        public double Amount { get; }
        public string Label { get; }
        public int Pair { get; }
        public int OperationType { get; }
    }
}