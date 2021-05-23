using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IHealable : IDamageable, INamed
    {
        void RestoreHealth(int healAmount, INamed source = null);
    }
}
