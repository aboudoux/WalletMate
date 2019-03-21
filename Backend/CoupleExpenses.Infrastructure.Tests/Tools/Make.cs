namespace CoupleExpenses.Infrastructure.Tests.Tools
{
    public class Make
    {
        public static TestEnvironment TestFile(string fileName)
        {
            return new TestEnvironment(fileName);
        }
    }
}