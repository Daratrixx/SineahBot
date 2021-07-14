using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SineahBot.Tools
{
    public static class Logging
    {

        public static void Log(string message)
        {
            using (var stream = File.AppendText($"./logs/{DateTime.Today.ToString("yyyy-MM-dd hh-mm-ss")}.log"))
            {
                stream.WriteLine(message);
            }
            Console.WriteLine(message);
        }
    }
}
