using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAgent : INamed
    {
        void Message(string message);
    }
}
