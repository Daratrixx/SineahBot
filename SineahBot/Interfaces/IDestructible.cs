using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IDestructible : IDamageable
    {
        void OnDestroyed();
    }
}
