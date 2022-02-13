using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SineahBot.Tools
{
    public static class Logging
    {
        public static readonly string SessionLogfilePath = $"../logs/{DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")}.log";

        private static readonly Mutex LoggingMutex = new Mutex();

        public static void Log(string message)
        {
            try
            {
                LoggingMutex.WaitOne();
                using (var stream = File.AppendText(SessionLogfilePath))
                {
                    stream.WriteLine(message);
                    Console.WriteLine(message);
                }
            }
            finally
            {
                LoggingMutex.ReleaseMutex();
            }
        }
        public static void Log(Exception e)
        {
            try
            {
                LoggingMutex.WaitOne();
                using (var stream = File.AppendText(SessionLogfilePath))
                {
                    while (e != null)
                    {
                        stream.WriteLine(e.Message);
                        stream.WriteLine(e.StackTrace);
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        e = e.InnerException;
                    }
                }
            }
            finally
            {
                LoggingMutex.ReleaseMutex();
            }
        }
    }
}
