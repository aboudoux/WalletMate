namespace WalletMate.Infrastructure.Dto
{
    public class Spending
    {
        public Spending(string periodId, double amount, string label, int pair, int category)
        {
            PeriodId = periodId;
            Amount = amount;
            Label = label;
            Pair = pair;
            Category = category;
        }

        public string PeriodId { get; }
        public double Amount { get; }
        public string Label { get; }
        public int Pair { get; }
        public int Category { get; }
    }
}