using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WalletMate.Infrastructure.Tests.Tools
{
    public class TestEnvironment
    {
        private readonly string _fileName;

        public string FilePath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _fileName);

        public TestEnvironment(string fileName)
        {
            _fileName = fileName;
        }

        public async Task AndExecute(Func<TestEnvironment, Task> action, bool deleteFile = true)
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