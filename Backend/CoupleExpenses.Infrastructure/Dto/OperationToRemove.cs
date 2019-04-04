namespace CoupleExpenses.Infrastructure.Dto
{
    public class OperationToRemove
    {
        public OperationToRemove(string periodId, int operationId)
        {
            PeriodId = periodId;
            OperationId = operationId;
        }

        public string PeriodId { get; }
        public int OperationId { get; }
    }
}