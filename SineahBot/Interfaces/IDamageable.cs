using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IDamageable : IAttackable
    {
        int OnDamage(int damageAmount, Entity damager = null);
    }
}
