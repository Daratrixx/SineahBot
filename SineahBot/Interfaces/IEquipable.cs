using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IEquipable : INamed
    {
        void OnEquipped(IAgent agent);
    }
}
