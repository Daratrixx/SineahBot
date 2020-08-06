using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAttackable
    {
        void OnAttacked(IAgent agent);
    }
}
