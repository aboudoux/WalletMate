namespace CoupleExpenses.Infrastructure.Tests
{
    public class Make
    {
        public static TestEnvironment TestFile(string fileName)
        {
            return new TestEnvironment(fileName);
        }
    }
}