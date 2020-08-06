using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAttacker
    {
        void OnAttacking(IAttackable attackable);
    }
}
