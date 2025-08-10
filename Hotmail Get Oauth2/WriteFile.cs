using System.IO;
using System.Reflection;
using System.Threading;

namespace Hotmail_Get_Oauth2
{
    public class WriteFile
    {
        private static ReaderWriterLock locker = new ReaderWriterLock();
        public void WriteOneByOne(string text, string nameFile_)
        {
            try
            {
                WriteFile.locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), nameFile_), new string[]
                {
                text
                });
            }
            finally
            {
                WriteFile.locker.ReleaseWriterLock();
            }
        }
    }
}
