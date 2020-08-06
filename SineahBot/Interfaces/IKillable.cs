using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IKillable
    {
        void OnKilled(IAgent agent);
    }
}
