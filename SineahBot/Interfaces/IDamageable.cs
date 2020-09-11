using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IDamageable : IAttackable
    {
        bool OnDamage(int damageAmount, INamed source = null);
    }
}
