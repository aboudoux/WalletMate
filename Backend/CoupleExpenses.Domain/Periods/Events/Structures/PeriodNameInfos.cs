namespace CoupleExpenses.Domain.Periods.Events.Structures
{
    public struct PeriodNameInfos
    {
        public PeriodNameInfos(int month, int year)
        {
            Month = month;
            Year = year;
        }

        public int Month { get; }
        public int Year { get; }
    }
}