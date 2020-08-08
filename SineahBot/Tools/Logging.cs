﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SineahBot.Tools
{
    public static class Logging
    {

        public static void Log(string message)
        {
            using (var stream = File.AppendText($"{DateTime.Today}.log"))
            {
                stream.WriteLine(message);
            }
            Console.WriteLine(message);
        }
    }
}
