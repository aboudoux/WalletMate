namespace WalletMate.Infrastructure.Tests.Tools
{
    public class Make
    {
        public static TestFile TestFile(string fileName)
        {
            return new TestFile(fileName);
        }
    }
}