using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAgent
    {
        void Message(string message);
        public string name { get; set; }
    }
}
