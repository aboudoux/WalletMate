namespace CoupleExpenses.WebApp.Dtos
{
    public class Period
    {
        public Period(int month, int year)
        {
            Month = month;
            Year = year;
        }
        public int Month { get;  }
        public int Year { get;  }
    }
}