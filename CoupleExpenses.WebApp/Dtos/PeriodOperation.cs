namespace CoupleExpenses.WebApp.Dtos
{
    public class PeriodOperation 
    {
        public PeriodOperation(string type, string pair, double amount, string label, string operationType)
        {
            Type = type;
            Pair = pair;
            Amount = amount;
            Label = label;
            OperationType = operationType;
        }

        public string Type { get; }
        public string Pair{get; }           
        public double Amount{get; }
        public string Label{get; }
        public string OperationType{get; }
    }
}