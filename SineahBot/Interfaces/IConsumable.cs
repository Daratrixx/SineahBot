using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IConsumable
    {
        void OnConsumed(IAgent agent);
    }
}
