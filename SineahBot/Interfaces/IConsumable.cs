using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IConsumable : INamed
    {
        void OnConsumed(IAgent agent);
    }
}
