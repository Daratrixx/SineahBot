using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IHealable : IAttackable, INamed
    {
        void OnHeal(int healAmount, INamed source = null);
    }
}
