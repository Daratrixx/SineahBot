using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAgent : INamed
    {
        void Message(string message);
        void RegisterMessageBypass(Action<Character, Room, string> handler, Action<Character, Room> cancelHandler = null);
    }
}
