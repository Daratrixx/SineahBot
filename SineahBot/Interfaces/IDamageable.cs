using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IDamageable : IAttackable
    {
        void OnDamage(int damageAmount, Entity damager = null);
    }
}
