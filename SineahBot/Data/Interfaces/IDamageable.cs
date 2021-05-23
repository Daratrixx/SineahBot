using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IDamageable
    {
        void DamageHealth(int damageAmount, DamageType type, INamed source = null);
    }
}
