using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAttacker : IObservable, INamed, IActionRateLimited
    {
        int GetWeaponDamage();
        void OnAttacking();
    }
}
