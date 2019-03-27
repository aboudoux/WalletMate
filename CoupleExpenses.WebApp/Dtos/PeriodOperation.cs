namespace CoupleExpenses.WebApp.Dtos
{
    public class PeriodOperation 
    {
        public string Type {get; set;}
        public string Pair{get; set;}           
        public double Amount{get; set;}
        public string Label{get; set;}
        public string OperationType{get; set;}
    }
}