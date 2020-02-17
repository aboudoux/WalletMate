using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WalletMate.Infrastructure.WebAppTests.Tools
{
    public class TestFile
    {
        private readonly string _fileName;

        public string FilePath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _fileName);

        public TestFile(string fileName)
        {
            _fileName = fileName;
        }

        public async Task AndExecute(Func<TestFile, Task> action, bool deleteFile = true)
        {
            try
            {
                await action(this);
            }
            finally
            {                
                if(deleteFile)
                    await DeleteFileSilently(FilePath);
            }
        }

        private Task DeleteFileSilently(string file)
        {
            try {
                File.Delete(file);
            }
            catch (Exception) {
            }
            return Task.CompletedTask;
        }
    }
}