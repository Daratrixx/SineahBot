using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IKillable : IDamageable
    {
        void OnKilled(IAgent agent = null);
    }
}
