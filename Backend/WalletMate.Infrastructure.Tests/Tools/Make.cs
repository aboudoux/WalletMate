namespace WalletMate.Infrastructure.WebAppTests.Tools
{
    public class Make
    {
        public static TestFile TestFile(string fileName)
        {
            return new TestFile(fileName);
        }
    }
}