using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotmail_Get_Oauth2
{
    class LoggingExtensions
    {

        static ReaderWriterLock locker = new ReaderWriterLock();
        public static void WriteDebug(string text)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);
                System.IO.File.AppendAllLines(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), "LinkBM.txt"), new[] { text });
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
    }
}
