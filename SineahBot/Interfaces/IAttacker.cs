using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAttacker : IObservable
    {
        void OnAttacking(IAttackable attackable);
    }
}
