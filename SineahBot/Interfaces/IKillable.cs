using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IKillable : IDamageable, INamed
    {
        void OnKilled(IAgent agent = null);
    }
}
