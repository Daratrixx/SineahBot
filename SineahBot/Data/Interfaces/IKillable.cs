using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IKillable : IDamageable, INamed
    {
        void OnKilled(Entity killer = null);
        bool IsDead();
    }
}
